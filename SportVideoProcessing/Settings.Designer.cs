namespace SportVideoProcessing
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.threesoldnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.noiseCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.changeColorButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.imageSettingsTabPage = new System.Windows.Forms.TabPage();
            this.numberSettingsTabPage = new System.Windows.Forms.TabPage();
            this.motionSettingsTabPage = new System.Windows.Forms.TabPage();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.threesoldnumericUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.imageSettingsTabPage.SuspendLayout();
            this.motionSettingsTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Разностное пороговое значение";
            // 
            // threesoldnumericUpDown
            // 
            this.threesoldnumericUpDown.Location = new System.Drawing.Point(6, 24);
            this.threesoldnumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.threesoldnumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.threesoldnumericUpDown.Name = "threesoldnumericUpDown";
            this.threesoldnumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.threesoldnumericUpDown.TabIndex = 1;
            this.threesoldnumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Подавление шума";
            // 
            // noiseCheckBox
            // 
            this.noiseCheckBox.AutoSize = true;
            this.noiseCheckBox.Checked = true;
            this.noiseCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noiseCheckBox.Location = new System.Drawing.Point(6, 63);
            this.noiseCheckBox.Name = "noiseCheckBox";
            this.noiseCheckBox.Size = new System.Drawing.Size(81, 17);
            this.noiseCheckBox.TabIndex = 5;
            this.noiseCheckBox.Text = "Подавлять";
            this.noiseCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(6, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(87, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Минимальный размер распознаваемой области";
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(5, 150);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(43, 20);
            this.widthTextBox.TabIndex = 9;
            this.widthTextBox.Text = "20";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ширина";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Высота";
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(60, 150);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(42, 20);
            this.heightTextBox.TabIndex = 12;
            this.heightTextBox.Text = "50";
            // 
            // changeColorButton
            // 
            this.changeColorButton.Location = new System.Drawing.Point(6, 86);
            this.changeColorButton.Name = "changeColorButton";
            this.changeColorButton.Size = new System.Drawing.Size(109, 23);
            this.changeColorButton.TabIndex = 13;
            this.changeColorButton.Text = "Изменить цвет";
            this.changeColorButton.UseVisualStyleBackColor = true;
            this.changeColorButton.Click += new System.EventHandler(this.changeColorButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.imageSettingsTabPage);
            this.tabControl1.Controls.Add(this.numberSettingsTabPage);
            this.tabControl1.Controls.Add(this.motionSettingsTabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(629, 307);
            this.tabControl1.TabIndex = 14;
            // 
            // imageSettingsTabPage
            // 
            this.imageSettingsTabPage.Controls.Add(this.groupBox1);
            this.imageSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.imageSettingsTabPage.Name = "imageSettingsTabPage";
            this.imageSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.imageSettingsTabPage.Size = new System.Drawing.Size(621, 281);
            this.imageSettingsTabPage.TabIndex = 0;
            this.imageSettingsTabPage.Text = "Обработка кадра";
            this.imageSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // numberSettingsTabPage
            // 
            this.numberSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.numberSettingsTabPage.Name = "numberSettingsTabPage";
            this.numberSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.numberSettingsTabPage.Size = new System.Drawing.Size(621, 227);
            this.numberSettingsTabPage.TabIndex = 1;
            this.numberSettingsTabPage.Text = "Распознавание цифр";
            this.numberSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // motionSettingsTabPage
            // 
            this.motionSettingsTabPage.Controls.Add(this.label2);
            this.motionSettingsTabPage.Controls.Add(this.changeColorButton);
            this.motionSettingsTabPage.Controls.Add(this.label1);
            this.motionSettingsTabPage.Controls.Add(this.heightTextBox);
            this.motionSettingsTabPage.Controls.Add(this.threesoldnumericUpDown);
            this.motionSettingsTabPage.Controls.Add(this.label5);
            this.motionSettingsTabPage.Controls.Add(this.label3);
            this.motionSettingsTabPage.Controls.Add(this.label4);
            this.motionSettingsTabPage.Controls.Add(this.noiseCheckBox);
            this.motionSettingsTabPage.Controls.Add(this.widthTextBox);
            this.motionSettingsTabPage.Controls.Add(this.button1);
            this.motionSettingsTabPage.Controls.Add(this.button2);
            this.motionSettingsTabPage.Location = new System.Drawing.Point(4, 22);
            this.motionSettingsTabPage.Name = "motionSettingsTabPage";
            this.motionSettingsTabPage.Size = new System.Drawing.Size(621, 281);
            this.motionSettingsTabPage.TabIndex = 2;
            this.motionSettingsTabPage.Text = "Распознавание движения";
            this.motionSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(165, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Автоматические настройки";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(159, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Пользовательский режим";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Тип настроек";
            // 
            // Settings
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(653, 331);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Настройки";
            ((System.ComponentModel.ISupportInitialize)(this.threesoldnumericUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.imageSettingsTabPage.ResumeLayout(false);
            this.motionSettingsTabPage.ResumeLayout(false);
            this.motionSettingsTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown threesoldnumericUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox noiseCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button changeColorButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage imageSettingsTabPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.TabPage numberSettingsTabPage;
        private System.Windows.Forms.TabPage motionSettingsTabPage;
    }
}