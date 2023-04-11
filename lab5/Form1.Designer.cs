namespace lab5
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ClientsListBox = new ListBox();
            DepartmentsListBox = new ListBox();
            ProjectsListBox = new ListBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            console = new ListBox();
            textBox1 = new TextBox();
            totalCash = new Label();
            StopAndSerialize = new Button();
            DeletingOldInformationTimer = new System.Windows.Forms.Timer(components);
            GenerateProjectsTimer = new System.Windows.Forms.Timer(components);
            continueExecuting = new Button();
            SuspendLayout();
            // 
            // ClientsListBox
            // 
            ClientsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ClientsListBox.FormattingEnabled = true;
            ClientsListBox.ItemHeight = 25;
            ClientsListBox.Location = new Point(10, 31);
            ClientsListBox.Name = "ClientsListBox";
            ClientsListBox.Size = new Size(182, 529);
            ClientsListBox.TabIndex = 0;
            ClientsListBox.SelectedIndexChanged += ClientsListBox_SelectedIndexChanged;
            // 
            // DepartmentsListBox
            // 
            DepartmentsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            DepartmentsListBox.FormattingEnabled = true;
            DepartmentsListBox.ItemHeight = 25;
            DepartmentsListBox.Location = new Point(386, 31);
            DepartmentsListBox.Name = "DepartmentsListBox";
            DepartmentsListBox.SelectionMode = SelectionMode.None;
            DepartmentsListBox.Size = new Size(182, 529);
            DepartmentsListBox.TabIndex = 0;
            DepartmentsListBox.SelectedIndexChanged += DepartmentsListBox_SelectedIndexChanged;
            // 
            // ProjectsListBox
            // 
            ProjectsListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ProjectsListBox.FormattingEnabled = true;
            ProjectsListBox.ItemHeight = 25;
            ProjectsListBox.Location = new Point(198, 31);
            ProjectsListBox.Name = "ProjectsListBox";
            ProjectsListBox.SelectionMode = SelectionMode.None;
            ProjectsListBox.Size = new Size(182, 529);
            ProjectsListBox.TabIndex = 0;
            ProjectsListBox.SelectedIndexChanged += ProjectsListBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 3);
            label1.Name = "label1";
            label1.Size = new Size(68, 25);
            label1.TabIndex = 1;
            label1.Text = "Clients:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(198, 3);
            label2.Name = "label2";
            label2.Size = new Size(78, 25);
            label2.TabIndex = 1;
            label2.Text = "Projects:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(386, 3);
            label3.Name = "label3";
            label3.Size = new Size(119, 25);
            label3.TabIndex = 1;
            label3.Text = "Departments:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(592, 3);
            label4.Name = "label4";
            label4.Size = new Size(80, 25);
            label4.TabIndex = 1;
            label4.Text = "Console:";
            // 
            // console
            // 
            console.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            console.FormattingEnabled = true;
            console.ItemHeight = 25;
            console.Location = new Point(592, 31);
            console.Name = "console";
            console.ScrollAlwaysVisible = true;
            console.Size = new Size(813, 529);
            console.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox1.Location = new Point(1185, 572);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(220, 31);
            textBox1.TabIndex = 6;
            // 
            // totalCash
            // 
            totalCash.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            totalCash.AutoSize = true;
            totalCash.Location = new Point(1032, 575);
            totalCash.Name = "totalCash";
            totalCash.Size = new Size(147, 25);
            totalCash.TabIndex = 7;
            totalCash.Text = "Сумарний дохід:";
            // 
            // StopAndSerialize
            // 
            StopAndSerialize.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            StopAndSerialize.Location = new Point(592, 567);
            StopAndSerialize.Name = "StopAndSerialize";
            StopAndSerialize.Size = new Size(253, 40);
            StopAndSerialize.TabIndex = 8;
            StopAndSerialize.Text = "Зупинити та зберегти звіт";
            StopAndSerialize.UseVisualStyleBackColor = true;
            StopAndSerialize.Click += StopAndSerialize_Click;
            // 
            // DeletingOldInformationTimer
            // 
            DeletingOldInformationTimer.Enabled = true;
            DeletingOldInformationTimer.Interval = 20000;
            DeletingOldInformationTimer.Tick += DeletingOldInformationTimer_Tick;
            // 
            // GenerateProjectsTimer
            // 
            GenerateProjectsTimer.Enabled = true;
            GenerateProjectsTimer.Interval = 3200;
            GenerateProjectsTimer.Tick += GenerateProjectsTimer_Tick;
            // 
            // continueExecuting
            // 
            continueExecuting.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            continueExecuting.Location = new Point(851, 567);
            continueExecuting.Name = "continueExecuting";
            continueExecuting.Size = new Size(175, 40);
            continueExecuting.TabIndex = 9;
            continueExecuting.Text = "Поновити роботу";
            continueExecuting.UseVisualStyleBackColor = true;
            continueExecuting.Click += continueExecuting_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1417, 622);
            Controls.Add(continueExecuting);
            Controls.Add(StopAndSerialize);
            Controls.Add(totalCash);
            Controls.Add(textBox1);
            Controls.Add(console);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(ProjectsListBox);
            Controls.Add(DepartmentsListBox);
            Controls.Add(ClientsListBox);
            MinimumSize = new Size(1439, 678);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Company Processing";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox ClientsListBox;
        private ListBox DepartmentsListBox;
        private ListBox ProjectsListBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ListBox console;
        private TextBox textBox1;
        private Label totalCash;
        private Button StopAndSerialize;
        private System.Windows.Forms.Timer DeletingOldInformationTimer;
        private System.Windows.Forms.Timer GenerateProjectsTimer;
        private Button continueExecuting;
    }
}