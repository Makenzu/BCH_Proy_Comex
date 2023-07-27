using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;

namespace BCH.Comex.Core.BL.XGCV
{
    public class Printer
    {
        public class TabInfo
        {
            public int Column { get; set; }
            public TabInfo()
            {
                Column = 0;
            }
            public TabInfo(short count)
            {
                Column = count;
            }
        }

        private class WordGroup
        {
            public string Content { get; set; }
            public Font Font { get; set; }

            public WordGroup(string content, Font font)
            {
                this.Content = content;
                this.Font = font;
            }
        }

        /// <summary>
        /// Linea del documento
        /// </summary>
        private class Line
        {
            public List<WordGroup> Content { get; set; }
            public bool NewPage { get; set; }

            public Line()
            {
                this.Content = new List<WordGroup>();
                this.NewPage = false;
            }

            //public Line(List<WordGroup> content, Font font)
            //{
            //    this.Content = content;
            //    this.NewPage = false;
            //}
            //public Line(List<WordGroup> content, Font font, bool isNewPage)
            //    : this(content, font)
            //{
            //    this.NewPage = isNewPage;
            //}
        }


        private static Printer printerInstance;
        public static Printer DefInstance 
        { 
            get
            {
                if (printerInstance == null)
                    printerInstance = new Printer();

                return printerInstance;
            }
        }

        private StringBuilder currentContent;
        private Line currentLine;
        private List<Line> document;

        private static string NewLine = "<br/>";
        private static string WhiteSpace = "&nsbp";

        //siempre es vbCentimetres
        public short ScaleMode { get; set; }
        /// <summary>
        /// Devuelve el nro de lineas y no la posicion exacta en esta versión simplificada
        /// </summary>
        public float CurrentY
        {
            get
            {
                // cantidad de lineas desde la ultima pagina
                int newPageLineIndex = document.FindLastIndex(x => x.NewPage);
                return document.Count - newPageLineIndex;
            }
        }
        public Font Font { get; set; }

        public Printer()
        {
            this.ScaleMode = 3;//vbCentimetres. se usa con CurrentY, pero se ignora para esta implementacion
            this.document = new List<Line>();
            this.currentLine = new Line();
            this.currentContent = new StringBuilder();
        }

        public static TabInfo TAB()
        {
            return new TabInfo();
        }

        public static TabInfo TAB(short count)
        {
            return new TabInfo(count);
        }

        public void Print()
        {
            NewLineContent();
        }

        public void Print(string input)
        {
            PrintWithoutCrlf(input);
            NewLineContent();
        }

        public void PrintList(object[] input)
        {
            PrintWithoutCrlf(input);
            NewLineContent();
        }

        public void PrintWithoutCrlf(params object[] input)
        {
            foreach (var item in input)
            {
                if (item == null)
                {
                    break;
                }
                else if (item is TabInfo)
                {
                    AddTabInfoToContent((TabInfo)item);
                }
                else
                {
                    AddStringToContent(item.ToString());
                }
            }

            AddToWordGroup();
        }

        /// <summary>
        /// Agrega un salto de pagina
        /// </summary>
        public void NewPage()
        {
            NewLineContent(true);
        }

        public void EndDoc()
        {
            NewLineContent();
        }

        /// <summary>
        /// Transforma el contenido a HTML
        /// </summary>
        public string ToHTML()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("<!DOCTYPE html>");
            output.AppendLine("<HTML>")
                .AppendLine("<BODY style='white-space: pre;'>");
            foreach (var item in this.document)
            {
                output.AppendLine("<p>");
                foreach (var wordGroup in item.Content)
                {
                    output.Append("<span style='")
                        .AppendFormat("font-family: {0}; font-size: {1}pt; ", 
                        wordGroup.Font.FontFamily.Name,
                        wordGroup.Font.SizeInPoints);
                    if (wordGroup.Font.Bold)
                        output.Append("font-weight: bold; ");
                    if (wordGroup.Font.Italic)
                        output.AppendFormat("font-style: italic; ");
                    if (wordGroup.Font.Underline) 
                        output.Append("text-decoration: underline;");
                    output.Append("'>");

                    output.Append(TransformToHTML(wordGroup.Content))
                        .Append("</span>");
                }
                output.AppendLine("</p>");
            }
            output.AppendLine("</BODY>")
                .AppendLine("</HTML>");

            return output.ToString();
        }

        /// <summary>
        /// Transforma la entrada a HMTL
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string TransformToHTML(string input)
        {
            return HttpUtility.HtmlEncode(input).ToString();
        }

        /// <summary>
        /// Cambia de linea
        /// </summary>
        private void NewLineContent()
        {
            bool isNewPage = this.document.Count == 0;

            this.document.Add(this.currentLine);

            this.currentLine = new Line();
            this.currentContent.Clear();
        }

        /// <summary>
        /// Cambia de linea
        /// </summary>
        private void NewLineContent(bool isNewPage)
        {
            this.currentLine.NewPage = isNewPage;

            this.document.Add(this.currentLine);

            this.currentLine = new Line();
            this.currentContent.Clear();
        }

        /// <summary>
        /// Agrega el contenido actual a un grupo de palabras
        /// </summary>
        private void AddToWordGroup()
        {
            this.currentLine.Content.Add(
                new WordGroup(this.currentContent.ToString(), this.Font)
                );

            this.currentContent.Clear();
        }

        /// <summary>
        /// Agrega contenido en forma de TabInfo
        /// </summary>
        /// <param name="tabInfo"></param>
        private void AddTabInfoToContent(TabInfo tabInfo)
        {
            short dif = (short)(tabInfo.Column - this.currentContent.Length);

            this.currentContent.Append(new string(' ', dif));
        }

        /// <summary>
        /// Agrega contenido en forma de string
        /// </summary>
        /// <param name="input"></param>
        private void AddStringToContent(string input)
        {
            this.currentContent.Append(input);
        }

    }
}
