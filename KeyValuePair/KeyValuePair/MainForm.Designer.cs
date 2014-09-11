namespace KeyValuePair
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label labelNameValuePairs;
            System.Windows.Forms.Label labelInput;
            System.Windows.Forms.Button buttonAdd;
            System.Windows.Forms.Button buttonSaveXml;
            System.Windows.Forms.Button buttonExit;
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonSortName = new System.Windows.Forms.Button();
            this.buttonSortValue = new System.Windows.Forms.Button();
            this.listBoxNameValuePairs = new System.Windows.Forms.ListBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            labelNameValuePairs = new System.Windows.Forms.Label();
            labelInput = new System.Windows.Forms.Label();
            buttonAdd = new System.Windows.Forms.Button();
            buttonSaveXml = new System.Windows.Forms.Button();
            buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNameValuePairs
            // 
            labelNameValuePairs.AutoSize = true;
            labelNameValuePairs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            labelNameValuePairs.Location = new System.Drawing.Point(13, 70);
            labelNameValuePairs.Name = "labelNameValuePairs";
            labelNameValuePairs.Size = new System.Drawing.Size(107, 13);
            labelNameValuePairs.TabIndex = 1;
            labelNameValuePairs.Text = "Name/Value Pair List";
            // 
            // labelInput
            // 
            labelInput.AutoSize = true;
            labelInput.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            labelInput.Location = new System.Drawing.Point(13, 13);
            labelInput.Name = "labelInput";
            labelInput.Size = new System.Drawing.Size(88, 13);
            labelInput.TabIndex = 2;
            labelInput.Text = "Name/Value Pair";
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            buttonAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonAdd.Location = new System.Drawing.Point(181, 27);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(91, 23);
            buttonAdd.TabIndex = 1;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.Enabled = false;
            this.buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDelete.Location = new System.Drawing.Point(182, 166);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(90, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonSaveXml
            // 
            buttonSaveXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            buttonSaveXml.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonSaveXml.Location = new System.Drawing.Point(182, 216);
            buttonSaveXml.Name = "buttonSaveXml";
            buttonSaveXml.Size = new System.Drawing.Size(90, 23);
            buttonSaveXml.TabIndex = 5;
            buttonSaveXml.Text = "Save as XML";
            buttonSaveXml.UseVisualStyleBackColor = true;
            buttonSaveXml.Click += new System.EventHandler(this.buttonSaveXml_Click);
            // 
            // buttonExit
            // 
            buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            buttonExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonExit.Location = new System.Drawing.Point(182, 266);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new System.Drawing.Size(90, 23);
            buttonExit.TabIndex = 6;
            buttonExit.Text = "Exit";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonSortName
            // 
            this.buttonSortName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSortName.Enabled = false;
            this.buttonSortName.Location = new System.Drawing.Point(181, 86);
            this.buttonSortName.Name = "buttonSortName";
            this.buttonSortName.Size = new System.Drawing.Size(91, 23);
            this.buttonSortName.TabIndex = 7;
            this.buttonSortName.Text = "Sort by Name";
            this.buttonSortName.UseVisualStyleBackColor = true;
            this.buttonSortName.Click += new System.EventHandler(this.buttonSortName_Click);
            // 
            // buttonSortValue
            // 
            this.buttonSortValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSortValue.Enabled = false;
            this.buttonSortValue.Location = new System.Drawing.Point(182, 116);
            this.buttonSortValue.Name = "buttonSortValue";
            this.buttonSortValue.Size = new System.Drawing.Size(90, 23);
            this.buttonSortValue.TabIndex = 8;
            this.buttonSortValue.Text = "Sort by Value";
            this.buttonSortValue.UseVisualStyleBackColor = true;
            this.buttonSortValue.Click += new System.EventHandler(this.buttonSortValue_Click);
            // 
            // listBoxNameValuePairs
            // 
            this.listBoxNameValuePairs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxNameValuePairs.CausesValidation = false;
            this.listBoxNameValuePairs.FormattingEnabled = true;
            this.listBoxNameValuePairs.Location = new System.Drawing.Point(16, 86);
            this.listBoxNameValuePairs.Name = "listBoxNameValuePairs";
            this.listBoxNameValuePairs.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxNameValuePairs.Size = new System.Drawing.Size(159, 199);
            this.listBoxNameValuePairs.TabIndex = 0;
            this.listBoxNameValuePairs.TabStop = false;
            this.listBoxNameValuePairs.SelectedIndexChanged += new System.EventHandler(this.listBoxNameValuePairs_SelectedIndexChanged);
            // 
            // textBoxInput
            // 
            this.textBoxInput.AllowDrop = true;
            this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInput.Location = new System.Drawing.Point(16, 30);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(159, 20);
            this.textBoxInput.TabIndex = 0;
            this.textBoxInput.Text = "Name=Value";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 302);
            this.Controls.Add(this.buttonSortValue);
            this.Controls.Add(this.buttonSortName);
            this.Controls.Add(buttonExit);
            this.Controls.Add(buttonSaveXml);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(buttonAdd);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(labelInput);
            this.Controls.Add(labelNameValuePairs);
            this.Controls.Add(this.listBoxNameValuePairs);
            this.MinimumSize = new System.Drawing.Size(300, 340);
            this.Name = "MainForm";
            this.Text = "Key-Value Pair Entry Program";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.ListBox listBoxNameValuePairs;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSortName;
        private System.Windows.Forms.Button buttonSortValue;


    }
}

