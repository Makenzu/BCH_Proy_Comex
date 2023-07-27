using System;
using System.Collections.Generic;
using System.Linq;

namespace BCH.Comex.Core.BL.SWEM.UI_Modulos
{
    public class UI_Control
    {
        public string ID { set; get; }
        public Object Tag { set; get; }
        public virtual bool Enabled { set; get; }
        public virtual bool Visible { set; get; }

        public UI_Control()
        {
            Enabled = true;
            Visible = true;
        }
    }

    public class UI_Frame : UI_Control
    {
        public UI_Frame()
            : base()
        {
            Controles = new List<UI_Control>();
            Enabled = true;
            Visible = true;
        }
        public string Caption { set; get; }
        public List<UI_Control> Controles { set; get; }

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }

            set
            {
                base.Visible = value;
                if (Controles != null)
                {
                    Controles.ForEach(x => x.Visible = value);
                }

            }
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }

            set
            {
                base.Enabled = value;
                if (Controles != null)
                {
                    Controles.ForEach(x => x.Enabled = value);
                }
            }
        }

    }

    public class UI_Container : UI_Control
    {

    }

    public class UI_Panel : UI_Control
    {
        public string Caption { set; get; }

        public UI_Panel()
        {
            this.Caption = string.Empty;
        }
    }

    public class UI_TextBox : UI_Control
    {
        public string Text { set; get; }
        public string Mask { get; set; }
        public int MaxLength { get; set; }
        public bool EsTextArea { get; set; }
        public int Rows { get; set; }

        public UI_TextBox()
        {
            Text = String.Empty;

        }
    }

    public class UI_Label : UI_Control
    {
        public string Text { set; get; }

        public UI_Label()
        {
            Text = String.Empty;
        }
    }

    public class UI_CheckBox : UI_Control
    {
        public short Value { get; set; }
        public bool Checked
        {
            get
            {
                return Value != 0;
            }
            set
            {
                Value = value ? (short)-1 : (short)0;
            }
        }
    }

    public class UI_OptionItem : UI_Control
    {
        public bool Selected { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }

        public UI_OptionItem()
        {
            Value = string.Empty;
        }
    }

    public class UI_GridItem : UI_Control
    {
        public Dictionary<string, string> columns { set; get; }
        public UI_GridItem()
        {
            columns = new Dictionary<string, string>();
        }

        public void AddColumn(string colName, string colVal)
        {
            columns.Add(colName, colVal);
        }

        public void SetValue(string colName, string value)
        {
            columns[colName] = value;
        }

        public string GetColumn<T>(string colName)
        {

            try
            {
                string aux;
                columns.TryGetValue(colName, out aux);
                return aux;
            }
            catch
            {
                return String.Empty;
            }
        }
    }

    public class UI_Button : UI_Control
    {
        public string Text { set; get; }
        public string ImgPath { get; set; }
        public UI_Button()
        {
            this.Text = String.Empty;
            this.Enabled = true;
            this.ID = String.Empty;
            this.Tag = String.Empty;
            this.ImgPath = string.Empty;
        }
    }

    public class UI_ComboItem : UI_Control
    {
        public string Value { set; get; }
        public int Data { get; set; }
    }

    public class UI_ListBoxItem : UI_Control
    {
        public string Value { set; get; }
        public int Data { get; set; }
    }

    public class UI_List : UI_Control
    {
        public int ListIndex
        {
            set;
            get;
        }
    }

    public class UI_Grid : UI_List
    {
        public List<UI_GridItem> Items { set; get; }
        public List<string> Header { get; set; }

        public int ListCount
        {
            get { return Items.Count; }
        }
        public void Clear()
        {
            Items.Clear();
        }
        public UI_Grid()
        {
            Items = new List<UI_GridItem>();
            Header = new List<string>();
        }
        public string get_ItemData(int index)
        {
            try
            {
                try
                {
                    return Items.ElementAt(index).ID;
                }
                catch
                {
                    return "-1";
                }
            }
            catch
            {
                return String.Empty;
            }

        }
    }

    public class UI_Combo : UI_List
    {
        private int _ListIndex;
        public string Text;
        public int ListCount
        {
            get { return Items.Count; }
        }
        public List<UI_ComboItem> Items { set; get; }

        /// <summary>
        /// Se usa en el binding de la vista, usando @Html.EditorFor<UI_Combo>
        /// </summary>
        public int? SelectedValue { set; get; }

        public int? Value
        {
            get
            {
                try
                {
                    return Items.ElementAt(ListIndex).Data;
                }
                catch
                {
                    return null;
                }
            }
        }

        public int ListIndex
        {
            set
            {
                _ListIndex = value;
                try
                {
                    Text = Items.ElementAt(_ListIndex).Value;
                }
                catch (Exception e)
                {
                    Text = String.Empty;
                }

            }
            get
            {
                return _ListIndex;
            }
        }

        public string get_ItemId(int index)
        {
            try
            {
                return Items.ElementAt(index).ID;
            }
            catch (Exception e)
            {
                return "-1";
            }
        }

        public int get_ItemData_(int index)
        {
            try
            {
                return Items.ElementAt(index).Data;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public int get_Index(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Convert.ToInt32(Items[i].ID) == id)
                {
                    return i;
                }
            }
            return 0;
        }

        public string get_List(int index)
        {
            if (index == -1)
                return string.Empty;
            else
                return Items.ElementAt(index).Value;
        }

        public void RemoveItem(short index)
        {
            Items.RemoveAt(index);
        }

        public UI_Combo()
        {
            Items = new List<UI_ComboItem>();
            ListIndex = -1;
        }

        public void AddItem(int data, string value)
        {
            UI_ComboItem item = new UI_ComboItem();
            item.Data = data;
            item.Value = value;
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }

    public class UI_ListBox : UI_List
    {
        private int _ListIndex;
        private string Text { get; set; }
        public int ListCount
        {
            get
            {
                return Items.Count;
            }
        }

        public List<UI_ListBoxItem> Items { set; get; }

        public int ListIndex
        {
            set
            {
                _ListIndex = value;
                try
                {
                    Text = Items.ElementAt(_ListIndex).Value;
                }
                catch (Exception e)
                {
                    Text = String.Empty;
                }

            }
            get
            {
                return _ListIndex;
            }
        }


        public int? SelectedValue { get; set; }

        public UI_ListBox()
        {
            Items = new List<UI_ListBoxItem>();
            _ListIndex = -1;
        }

        public int get_ItemData(int index)
        {
            try
            {
                return Items.ElementAt(index).Data;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public void set_ItemData(int index, string data)
        {
            UI_ListBoxItem item = new UI_ListBoxItem();
            item.Data = index;
            item.Value = data;
            Items.Add(item);
        }

        public string get_List(int index)
        {
            if (index == -1)
                return string.Empty;
            else
                return Items.ElementAt(index).Value;
        }

        public void Clear()
        {
            Items.Clear();
        }

    }

    public class UI_PictureBox
    {
        public bool Enabled;
    }

    public class UI_GridList : UI_Control
    {
        //public List<UI_GridListItem> Items { set; get; }
        //public List<string> Header { get; set; }
        //public int ListIndex { set; get; }
        //public UI_GridList()
        //{
        //    Items = new List<UI_GridListItem>();
        //    Header = new List<string>();
        //}
    }
}
