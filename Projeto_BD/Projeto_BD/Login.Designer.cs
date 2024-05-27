namespace Projeto_BD
{
    partial class Login
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
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            username_label = new Label();
            password_label = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Franklin Gothic Book", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(263, 401);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(177, 96);
            button1.TabIndex = 0;
            button1.Text = "Submit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Franklin Gothic Book", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(459, 401);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(177, 96);
            button2.TabIndex = 1;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(385, 247);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(251, 27);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(385, 299);
            textBox2.Margin = new Padding(3, 4, 3, 4);
            textBox2.Name = "textBox2";
            textBox2.PasswordChar = '*';
            textBox2.Size = new Size(251, 27);
            textBox2.TabIndex = 3;
            // 
            // username_label
            // 
            username_label.AutoSize = true;
            username_label.Font = new Font("Franklin Gothic Book", 12F, FontStyle.Regular, GraphicsUnit.Point);
            username_label.Location = new Point(274, 245);
            username_label.Name = "username_label";
            username_label.Size = new Size(105, 25);
            username_label.TabIndex = 4;
            username_label.Text = "ID Festival:";
            username_label.Click += label1_Click;
            // 
            // password_label
            // 
            password_label.AutoSize = true;
            password_label.Font = new Font("Franklin Gothic Book", 12F, FontStyle.Regular, GraphicsUnit.Point);
            password_label.Location = new Point(274, 297);
            password_label.Name = "password_label";
            password_label.Size = new Size(98, 25);
            password_label.TabIndex = 5;
            password_label.Text = "Password:";
            password_label.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Franklin Gothic Book", 29.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(307, 104);
            label1.Name = "label1";
            label1.Size = new Size(336, 62);
            label1.TabIndex = 6;
            label1.Text = "SUMMERFEST";
            label1.Click += label1_Click_1;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.summerfest_grounds;
            pictureBox1.Location = new Point(-5, -196);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(925, 792);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(label1);
            Controls.Add(password_label);
            Controls.Add(username_label);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pictureBox1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Login";
            Text = "Form1";
            Load += Login_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label username_label;
        private Label password_label;
        private Label label1;
        private PictureBox pictureBox1;
    }
}