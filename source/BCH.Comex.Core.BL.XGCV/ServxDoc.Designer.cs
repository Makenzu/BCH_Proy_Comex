
namespace BCH.Comex.Core.BL.XGCV
{
   #region Windows Form Designer generated code
   //partial class ServxDoc
   //{
   //   private static ServxDoc m_vb6FormDefInstance = null;
   //   public static ServxDoc DefInstance
   //   {
   //      get
   //      {
   //         if(m_vb6FormDefInstance == null || m_vb6FormDefInstance.IsDisposed)
   //         {
   //            m_vb6FormDefInstance = new ServxDoc();
   //         }
   //         return m_vb6FormDefInstance;
   //      }
   //      set
   //      {
   //         m_vb6FormDefInstance = value;
   //      }
   //   }
   //   /// <summary>
   //   /// Required designer variable.
   //   /// </summary>
   //   private System.ComponentModel.IContainer components = null;
   //   private System.Windows.Forms.TextBox correlativo;
   //   private System.Windows.Forms.TextBox operacion;
   //   private System.Windows.Forms.Button Co_Documentos;
   //   private System.Windows.Forms.TextBox CajaMultilinea;
   //   private System.Windows.Forms.Button Co_Salir;
   //   private System.Windows.Forms.TextBox Text1;
   //   private System.Windows.Forms.TextBox impresora;
   //   private System.Windows.Forms.TextBox documento;

   //   /// <summary>
   //   /// Required method for Designer support - do not modify
   //   /// the contents of this method with the code editor.
   //   /// </summary>
   //   private void InitializeComponent()
   //   {
   //      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServxDoc));
   //      this.components = new System.ComponentModel.Container();
   //      this.correlativo = new System.Windows.Forms.TextBox();
   //      this.operacion = new System.Windows.Forms.TextBox();
   //      this.Co_Documentos = new System.Windows.Forms.Button();
   //      this.CajaMultilinea = new System.Windows.Forms.TextBox();
   //      this.Co_Salir = new System.Windows.Forms.Button();
   //      this.Text1 = new System.Windows.Forms.TextBox();
   //      this.impresora = new System.Windows.Forms.TextBox();
   //      this.documento = new System.Windows.Forms.TextBox();
   //      this.SuspendLayout();
   //      //
   //      // correlativo
   //      //
   //      this.correlativo.AutoSize = false;
   //      this.correlativo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
   //      this.correlativo.BackColor = System.Drawing.SystemColors.Window;
   //      this.correlativo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.correlativo.Size = new System.Drawing.Size(67, 21);
   //      this.correlativo.Location = new System.Drawing.Point(232, 23);
   //      this.correlativo.MaxLength = 0;
   //      this.correlativo.TabIndex = 2;
   //      this.correlativo.AcceptsReturn = true;
   //      this.correlativo.CausesValidation = true;
   //      this.correlativo.Enabled = true;
   //      this.correlativo.ForeColor = System.Drawing.SystemColors.WindowText;
   //      this.correlativo.HideSelection = true;
   //      this.correlativo.ReadOnly = false;
   //      this.correlativo.Cursor = System.Windows.Forms.Cursors.IBeam;
   //      this.correlativo.Multiline = false;
   //      this.correlativo.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.correlativo.ScrollBars = System.Windows.Forms.ScrollBars.None;
   //      this.correlativo.TabStop = true;
   //      this.correlativo.Visible = true;
   //      this.correlativo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
   //      this.correlativo.Name = "correlativo";
   //      this.correlativo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.correlativo_KeyPress);
   //      //
   //      // operacion
   //      //
   //      this.operacion.AutoSize = false;
   //      this.operacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
   //      this.operacion.BackColor = System.Drawing.SystemColors.Window;
   //      this.operacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.operacion.Size = new System.Drawing.Size(125, 21);
   //      this.operacion.Location = new System.Drawing.Point(28, 23);
   //      this.operacion.MaxLength = 0;
   //      this.operacion.TabIndex = 0;
   //      this.operacion.AcceptsReturn = true;
   //      this.operacion.CausesValidation = true;
   //      this.operacion.Enabled = true;
   //      this.operacion.ForeColor = System.Drawing.SystemColors.WindowText;
   //      this.operacion.HideSelection = true;
   //      this.operacion.ReadOnly = false;
   //      this.operacion.Cursor = System.Windows.Forms.Cursors.IBeam;
   //      this.operacion.Multiline = false;
   //      this.operacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.operacion.ScrollBars = System.Windows.Forms.ScrollBars.None;
   //      this.operacion.TabStop = true;
   //      this.operacion.Visible = true;
   //      this.operacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
   //      this.operacion.Name = "operacion";
   //      this.operacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.operacion_KeyPress);
   //      //
   //      // Co_Documentos
   //      //
   //      this.Co_Documentos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
   //      this.Co_Documentos.FlatStyle = System.Windows.Forms.FlatStyle.System;
   //      this.Co_Documentos.BackColor = System.Drawing.SystemColors.Window;
   //      this.Co_Documentos.Text = "&Documentos";
   //      this.Co_Documentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.Co_Documentos.Size = new System.Drawing.Size(73, 23);
   //      this.Co_Documentos.Location = new System.Drawing.Point(32, 50);
   //      this.Co_Documentos.TabIndex = 4;
   //      this.Co_Documentos.TabStop = true;
   //      this.Co_Documentos.CausesValidation = true;
   //      this.Co_Documentos.Enabled = true;
   //      this.Co_Documentos.ForeColor = System.Drawing.SystemColors.ControlText;
   //      this.Co_Documentos.Cursor = System.Windows.Forms.Cursors.Default;
   //      this.Co_Documentos.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.Co_Documentos.Name = "Co_Documentos";
   //      this.Co_Documentos.Click += new System.EventHandler(this.Co_Documentos_Click);
   //      //
   //      // CajaMultilinea
   //      //
   //      this.CajaMultilinea.AutoSize = false;
   //      this.CajaMultilinea.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
   //      this.CajaMultilinea.BackColor = System.Drawing.SystemColors.Window;
   //      this.CajaMultilinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.CajaMultilinea.Size = new System.Drawing.Size(584, 66);
   //      this.CajaMultilinea.Location = new System.Drawing.Point(3, 269);
   //      this.CajaMultilinea.MaxLength = 0;
   //      this.CajaMultilinea.TabIndex = 6;
   //      this.CajaMultilinea.AcceptsReturn = true;
   //      this.CajaMultilinea.CausesValidation = true;
   //      this.CajaMultilinea.Enabled = true;
   //      this.CajaMultilinea.ForeColor = System.Drawing.SystemColors.WindowText;
   //      this.CajaMultilinea.HideSelection = true;
   //      this.CajaMultilinea.ReadOnly = false;
   //      this.CajaMultilinea.Cursor = System.Windows.Forms.Cursors.IBeam;
   //      this.CajaMultilinea.Multiline = true;
   //      this.CajaMultilinea.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.CajaMultilinea.ScrollBars = System.Windows.Forms.ScrollBars.None;
   //      this.CajaMultilinea.TabStop = false;
   //      this.CajaMultilinea.Visible = true;
   //      this.CajaMultilinea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
   //      this.CajaMultilinea.Name = "CajaMultilinea";
   //      //
   //      // Co_Salir
   //      //
   //      this.Co_Salir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
   //      this.Co_Salir.FlatStyle = System.Windows.Forms.FlatStyle.System;
   //      this.Co_Salir.BackColor = System.Drawing.SystemColors.Window;
   //      this.Co_Salir.Text = "&Salir";
   //      this.Co_Salir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.Co_Salir.Size = new System.Drawing.Size(73, 23);
   //      this.Co_Salir.Location = new System.Drawing.Point(112, 50);
   //      this.Co_Salir.TabIndex = 5;
   //      this.Co_Salir.TabStop = true;
   //      this.Co_Salir.CausesValidation = true;
   //      this.Co_Salir.Enabled = true;
   //      this.Co_Salir.ForeColor = System.Drawing.SystemColors.ControlText;
   //      this.Co_Salir.Cursor = System.Windows.Forms.Cursors.Default;
   //      this.Co_Salir.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.Co_Salir.Name = "Co_Salir";
   //      this.Co_Salir.Click += new System.EventHandler(this.Co_Salir_Click);
   //      //
   //      // Text1
   //      //
   //      this.Text1.AutoSize = false;
   //      this.Text1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
   //      this.Text1.BackColor = System.Drawing.SystemColors.Window;
   //      this.Text1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.Text1.Size = new System.Drawing.Size(294, 20);
   //      this.Text1.Location = new System.Drawing.Point(88, 200);
   //      this.Text1.MaxLength = 0;
   //      this.Text1.TabIndex = 7;
   //      this.Text1.Text = "Text1";
   //      this.Text1.AcceptsReturn = true;
   //      this.Text1.CausesValidation = true;
   //      this.Text1.Enabled = true;
   //      this.Text1.ForeColor = System.Drawing.SystemColors.WindowText;
   //      this.Text1.HideSelection = true;
   //      this.Text1.ReadOnly = false;
   //      this.Text1.Cursor = System.Windows.Forms.Cursors.IBeam;
   //      this.Text1.Multiline = false;
   //      this.Text1.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.Text1.ScrollBars = System.Windows.Forms.ScrollBars.None;
   //      this.Text1.TabStop = true;
   //      this.Text1.Visible = true;
   //      this.Text1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
   //      this.Text1.Name = "Text1";
   //      //
   //      // impresora
   //      //
   //      this.impresora.AutoSize = false;
   //      this.impresora.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
   //      this.impresora.BackColor = System.Drawing.SystemColors.Window;
   //      this.impresora.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.impresora.Size = new System.Drawing.Size(67, 21);
   //      this.impresora.Location = new System.Drawing.Point(306, 23);
   //      this.impresora.MaxLength = 0;
   //      this.impresora.TabIndex = 3;
   //      this.impresora.AcceptsReturn = true;
   //      this.impresora.CausesValidation = true;
   //      this.impresora.Enabled = true;
   //      this.impresora.ForeColor = System.Drawing.SystemColors.WindowText;
   //      this.impresora.HideSelection = true;
   //      this.impresora.ReadOnly = false;
   //      this.impresora.Cursor = System.Windows.Forms.Cursors.IBeam;
   //      this.impresora.Multiline = false;
   //      this.impresora.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.impresora.ScrollBars = System.Windows.Forms.ScrollBars.None;
   //      this.impresora.TabStop = true;
   //      this.impresora.Visible = true;
   //      this.impresora.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
   //      this.impresora.Name = "impresora";
   //      this.impresora.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.impresora_KeyPress);
   //      //
   //      // documento
   //      //
   //      this.documento.AutoSize = false;
   //      this.documento.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
   //      this.documento.BackColor = System.Drawing.SystemColors.Window;
   //      this.documento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (System.Byte)(0));
   //      this.documento.Size = new System.Drawing.Size(67, 21);
   //      this.documento.Location = new System.Drawing.Point(160, 23);
   //      this.documento.MaxLength = 0;
   //      this.documento.TabIndex = 1;
   //      this.documento.AcceptsReturn = true;
   //      this.documento.CausesValidation = true;
   //      this.documento.Enabled = true;
   //      this.documento.ForeColor = System.Drawing.SystemColors.WindowText;
   //      this.documento.HideSelection = true;
   //      this.documento.ReadOnly = false;
   //      this.documento.Cursor = System.Windows.Forms.Cursors.IBeam;
   //      this.documento.Multiline = false;
   //      this.documento.RightToLeft = System.Windows.Forms.RightToLeft.No;
   //      this.documento.ScrollBars = System.Windows.Forms.ScrollBars.None;
   //      this.documento.TabStop = true;
   //      this.documento.Visible = true;
   //      this.documento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
   //      this.documento.Name = "documento";
   //      this.documento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.documento_KeyPress);
   //      //
   //      // ServxDoc
   //      //
   //      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
   //      this.ClientSize = new System.Drawing.Size(396, 110);
   //      this.Controls.AddRange(new System.Windows.Forms.Control[] {
   //         this.correlativo,
   //         this.operacion,
   //         this.Co_Documentos,
   //         this.CajaMultilinea,
   //         this.Co_Salir,
   //         this.Text1,
   //         this.impresora,
   //         this.documento
   //      });
   //      this.Name = "ServxDoc";
   //      this.Text = "Impresión Exportaciones";
   //      this.BackColor = System.Drawing.SystemColors.Window;
   //      this.ForeColor = System.Drawing.SystemColors.WindowText;
   //      this.Left = 57;
   //      this.Top = 203;
   //      this.Width = 404;
   //      this.Height = 137;
   //      this.Icon = (System.Drawing.Icon)resources.GetObject("FrxData0000.Icon");
   //      this.Font = new System.Drawing.Font("MS Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,(System.Byte)(0));
   //      this.CancelButton = this.Co_Salir;
   //      //this.LinkExecute += new System.EventHandler(this.Form_LinkExecute);
   //      this.ResumeLayout(false);

   //      this.ResumeLayout(false);
   //      this.PerformLayout();
   //   }
   //}
   #endregion
}
