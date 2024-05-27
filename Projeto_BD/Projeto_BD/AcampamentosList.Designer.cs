namespace Projeto_BD
{
    partial class AcampamentosList
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
            campList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)campList).BeginInit();
            SuspendLayout();
            // 
            // campList
            // 
            campList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            campList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            campList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            campList.Location = new Point(12, 12);
            campList.Name = "campList";
            campList.RowHeadersWidth = 51;
            campList.RowTemplate.Height = 29;
            campList.Size = new Size(776, 185);
            campList.TabIndex = 0;
            campList.CellContentClick += campList_CellContentClick;
            // 
            // AcampamentosList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 237);
            Controls.Add(campList);
            Name = "AcampamentosList";
            Text = "Form1";
            Load += AcampamentosList_Load;
            ((System.ComponentModel.ISupportInitialize)campList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView campList;
    }
}