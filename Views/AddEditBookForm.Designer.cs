namespace LibraryManagement.Views;

partial class AddEditBookForm
{
	private System.ComponentModel.IContainer components = null;
	private TableLayoutPanel mainLayout = null!;
	private Label titleLabel = null!;
	private TextBox titleTextBox = null!;
	private Label titleErrorLabel = null!;
	private Label authorLabel = null!;
	private TextBox authorTextBox = null!;
	private Label authorErrorLabel = null!;
	private Label isbnLabel = null!;
	private TextBox isbnTextBox = null!;
	private Label isbnErrorLabel = null!;
	private Label yearLabel = null!;
	private FlowLayoutPanel yearPanel = null!;
	private CheckBox yearCheckBox = null!;
	private NumericUpDown yearNumericUpDown = null!;
	private Label yearErrorLabel = null!;
	private Label genreLabel = null!;
	private TextBox genreTextBox = null!;
	private Label rayonLabel = null!;
	private TextBox rayonTextBox = null!;
	private Label etagereLabel = null!;
	private TextBox etagereTextBox = null!;
	private CheckBox availableCheckBox = null!;
	private Label coverPathLabel = null!;
	private FlowLayoutPanel coverPathPanel = null!;
	private TextBox coverPathTextBox = null!;
	private Button browseCoverButton = null!;
	private PictureBox coverPreviewBox = null!;
	private FlowLayoutPanel buttonPanel = null!;
	private Button saveButton = null!;
	private Button cancelButton = null!;

	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}

		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		mainLayout = new TableLayoutPanel();
		titleLabel = new Label();
		titleTextBox = new TextBox();
		titleErrorLabel = new Label();
		authorLabel = new Label();
		authorTextBox = new TextBox();
		authorErrorLabel = new Label();
		isbnLabel = new Label();
		isbnTextBox = new TextBox();
		isbnErrorLabel = new Label();
		yearLabel = new Label();
		yearPanel = new FlowLayoutPanel();
		yearCheckBox = new CheckBox();
		yearNumericUpDown = new NumericUpDown();
		yearErrorLabel = new Label();
		genreLabel = new Label();
		genreTextBox = new TextBox();
		rayonLabel = new Label();
		rayonTextBox = new TextBox();
		etagereLabel = new Label();
		etagereTextBox = new TextBox();
		availableCheckBox = new CheckBox();
		coverPathLabel = new Label();
		coverPathPanel = new FlowLayoutPanel();
		coverPathTextBox = new TextBox();
		browseCoverButton = new Button();
		coverPreviewBox = new PictureBox();
		buttonPanel = new FlowLayoutPanel();
		saveButton = new Button();
		cancelButton = new Button();
		mainLayout.SuspendLayout();
		coverPathPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)coverPreviewBox).BeginInit();
		yearPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)yearNumericUpDown).BeginInit();
		buttonPanel.SuspendLayout();
		SuspendLayout();
		// 
		// mainLayout
		// 
		mainLayout.ColumnCount = 2;
		mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
		mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		mainLayout.Controls.Add(titleLabel, 0, 0);
		mainLayout.Controls.Add(titleTextBox, 1, 0);
		mainLayout.Controls.Add(titleErrorLabel, 1, 1);
		mainLayout.Controls.Add(authorLabel, 0, 2);
		mainLayout.Controls.Add(authorTextBox, 1, 2);
		mainLayout.Controls.Add(authorErrorLabel, 1, 3);
		mainLayout.Controls.Add(isbnLabel, 0, 4);
		mainLayout.Controls.Add(isbnTextBox, 1, 4);
		mainLayout.Controls.Add(isbnErrorLabel, 1, 5);
		mainLayout.Controls.Add(yearLabel, 0, 6);
		mainLayout.Controls.Add(yearPanel, 1, 6);
		mainLayout.Controls.Add(yearErrorLabel, 1, 7);
		mainLayout.Controls.Add(genreLabel, 0, 8);
		mainLayout.Controls.Add(genreTextBox, 1, 8);
		mainLayout.Controls.Add(rayonLabel, 0, 9);
		mainLayout.Controls.Add(rayonTextBox, 1, 9);
		mainLayout.Controls.Add(etagereLabel, 0, 10);
		mainLayout.Controls.Add(etagereTextBox, 1, 10);
		mainLayout.Controls.Add(availableCheckBox, 1, 11);
		mainLayout.Controls.Add(coverPathLabel, 0, 12);
		mainLayout.Controls.Add(coverPathPanel, 1, 12);
		mainLayout.Controls.Add(coverPreviewBox, 1, 13);
		mainLayout.Controls.Add(buttonPanel, 1, 14);
		mainLayout.Dock = DockStyle.Fill;
		mainLayout.Location = new Point(0, 0);
		mainLayout.Name = "mainLayout";
		mainLayout.RowCount = 15;
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
		mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
		mainLayout.Size = new Size(620, 524);
		mainLayout.TabIndex = 0;
		// 
		// titleLabel
		// 
		titleLabel.Anchor = AnchorStyles.Left;
		titleLabel.AutoSize = true;
		titleLabel.Location = new Point(12, 9);
		titleLabel.Name = "titleLabel";
		titleLabel.Size = new Size(33, 15);
		titleLabel.TabIndex = 0;
		titleLabel.Text = "Title";
		// 
		// titleTextBox
		// 
		titleTextBox.Dock = DockStyle.Fill;
		titleTextBox.Location = new Point(123, 6);
		titleTextBox.Margin = new Padding(3, 6, 12, 6);
		titleTextBox.Name = "titleTextBox";
		titleTextBox.Size = new Size(485, 23);
		titleTextBox.TabIndex = 1;
		// 
		// titleErrorLabel
		// 
		titleErrorLabel.AutoSize = true;
		titleErrorLabel.Dock = DockStyle.Fill;
		titleErrorLabel.ForeColor = Color.Firebrick;
		titleErrorLabel.Location = new Point(123, 34);
		titleErrorLabel.Name = "titleErrorLabel";
		titleErrorLabel.Size = new Size(485, 20);
		titleErrorLabel.TabIndex = 2;
		// 
		// authorLabel
		// 
		authorLabel.Anchor = AnchorStyles.Left;
		authorLabel.AutoSize = true;
		authorLabel.Location = new Point(12, 57);
		authorLabel.Name = "authorLabel";
		authorLabel.Size = new Size(45, 15);
		authorLabel.TabIndex = 3;
		authorLabel.Text = "Author";
		// 
		// authorTextBox
		// 
		authorTextBox.Dock = DockStyle.Fill;
		authorTextBox.Location = new Point(123, 54);
		authorTextBox.Margin = new Padding(3, 6, 12, 6);
		authorTextBox.Name = "authorTextBox";
		authorTextBox.Size = new Size(485, 23);
		authorTextBox.TabIndex = 4;
		// 
		// authorErrorLabel
		// 
		authorErrorLabel.AutoSize = true;
		authorErrorLabel.Dock = DockStyle.Fill;
		authorErrorLabel.ForeColor = Color.Firebrick;
		authorErrorLabel.Location = new Point(123, 82);
		authorErrorLabel.Name = "authorErrorLabel";
		authorErrorLabel.Size = new Size(485, 20);
		authorErrorLabel.TabIndex = 5;
		// 
		// isbnLabel
		// 
		isbnLabel.Anchor = AnchorStyles.Left;
		isbnLabel.AutoSize = true;
		isbnLabel.Location = new Point(12, 105);
		isbnLabel.Name = "isbnLabel";
		isbnLabel.Size = new Size(31, 15);
		isbnLabel.TabIndex = 6;
		isbnLabel.Text = "ISBN";
		// 
		// isbnTextBox
		// 
		isbnTextBox.Dock = DockStyle.Fill;
		isbnTextBox.Location = new Point(123, 102);
		isbnTextBox.Margin = new Padding(3, 6, 12, 6);
		isbnTextBox.Name = "isbnTextBox";
		isbnTextBox.Size = new Size(485, 23);
		isbnTextBox.TabIndex = 7;
		// 
		// isbnErrorLabel
		// 
		isbnErrorLabel.AutoSize = true;
		isbnErrorLabel.Dock = DockStyle.Fill;
		isbnErrorLabel.ForeColor = Color.Firebrick;
		isbnErrorLabel.Location = new Point(123, 130);
		isbnErrorLabel.Name = "isbnErrorLabel";
		isbnErrorLabel.Size = new Size(485, 20);
		isbnErrorLabel.TabIndex = 8;
		// 
		// yearLabel
		// 
		yearLabel.Anchor = AnchorStyles.Left;
		yearLabel.AutoSize = true;
		yearLabel.Location = new Point(12, 153);
		yearLabel.Name = "yearLabel";
		yearLabel.Size = new Size(29, 15);
		yearLabel.TabIndex = 9;
		yearLabel.Text = "Year";
		// 
		// yearPanel
		// 
		yearPanel.Controls.Add(yearCheckBox);
		yearPanel.Controls.Add(yearNumericUpDown);
		yearPanel.Dock = DockStyle.Fill;
		yearPanel.Location = new Point(123, 150);
		yearPanel.Margin = new Padding(3, 6, 12, 6);
		yearPanel.Name = "yearPanel";
		yearPanel.Size = new Size(485, 22);
		yearPanel.TabIndex = 10;
		// 
		// yearCheckBox
		// 
		yearCheckBox.AutoSize = true;
		yearCheckBox.Location = new Point(3, 3);
		yearCheckBox.Name = "yearCheckBox";
		yearCheckBox.Size = new Size(60, 19);
		yearCheckBox.TabIndex = 0;
		yearCheckBox.Text = "Use year";
		yearCheckBox.UseVisualStyleBackColor = true;
		yearCheckBox.CheckedChanged += yearCheckBox_CheckedChanged;
		// 
		// yearNumericUpDown
		// 
		yearNumericUpDown.Enabled = false;
		yearNumericUpDown.Location = new Point(69, 1);
		yearNumericUpDown.Maximum = new decimal(new int[] { 3000, 0, 0, 0 });
		yearNumericUpDown.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
		yearNumericUpDown.Name = "yearNumericUpDown";
		yearNumericUpDown.Size = new Size(90, 23);
		yearNumericUpDown.TabIndex = 1;
		yearNumericUpDown.Value = new decimal(new int[] { 2024, 0, 0, 0 });
		// 
		// yearErrorLabel
		// 
		yearErrorLabel.AutoSize = true;
		yearErrorLabel.Dock = DockStyle.Fill;
		yearErrorLabel.ForeColor = Color.Firebrick;
		yearErrorLabel.Location = new Point(123, 178);
		yearErrorLabel.Name = "yearErrorLabel";
		yearErrorLabel.Size = new Size(485, 20);
		yearErrorLabel.TabIndex = 11;
		// 
		// genreLabel
		// 
		genreLabel.Anchor = AnchorStyles.Left;
		genreLabel.AutoSize = true;
		genreLabel.Location = new Point(12, 202);
		genreLabel.Name = "genreLabel";
		genreLabel.Size = new Size(39, 15);
		genreLabel.TabIndex = 12;
		genreLabel.Text = "Genre";
		// 
		// genreTextBox
		// 
		genreTextBox.Dock = DockStyle.Fill;
		genreTextBox.Location = new Point(123, 197);
		genreTextBox.Margin = new Padding(3, 6, 12, 6);
		genreTextBox.Name = "genreTextBox";
		genreTextBox.Size = new Size(485, 23);
		genreTextBox.TabIndex = 13;
		// 
		// rayonLabel
		// 
		rayonLabel.Anchor = AnchorStyles.Left;
		rayonLabel.AutoSize = true;
		rayonLabel.Location = new Point(12, 236);
		rayonLabel.Name = "rayonLabel";
		rayonLabel.Size = new Size(40, 15);
		rayonLabel.TabIndex = 14;
		rayonLabel.Text = "Rayon";
		// 
		// rayonTextBox
		// 
		rayonTextBox.Dock = DockStyle.Fill;
		rayonTextBox.Location = new Point(123, 231);
		rayonTextBox.Margin = new Padding(3, 6, 12, 6);
		rayonTextBox.Name = "rayonTextBox";
		rayonTextBox.Size = new Size(485, 23);
		rayonTextBox.TabIndex = 15;
		// 
		// etagereLabel
		// 
		etagereLabel.Anchor = AnchorStyles.Left;
		etagereLabel.AutoSize = true;
		etagereLabel.Location = new Point(12, 270);
		etagereLabel.Name = "etagereLabel";
		etagereLabel.Size = new Size(50, 15);
		etagereLabel.TabIndex = 16;
		etagereLabel.Text = "Etagere";
		// 
		// etagereTextBox
		// 
		etagereTextBox.Dock = DockStyle.Fill;
		etagereTextBox.Location = new Point(123, 265);
		etagereTextBox.Margin = new Padding(3, 6, 12, 6);
		etagereTextBox.Name = "etagereTextBox";
		etagereTextBox.Size = new Size(485, 23);
		etagereTextBox.TabIndex = 17;
		// 
		// availableCheckBox
		// 
		availableCheckBox.AutoSize = true;
		availableCheckBox.Checked = true;
		availableCheckBox.CheckState = CheckState.Checked;
		availableCheckBox.Location = new Point(123, 299);
		availableCheckBox.Margin = new Padding(3, 4, 3, 4);
		availableCheckBox.Name = "availableCheckBox";
		availableCheckBox.Size = new Size(70, 19);
		availableCheckBox.TabIndex = 18;
		availableCheckBox.Text = "Available";
		availableCheckBox.UseVisualStyleBackColor = true;
		// 
		// coverPathLabel
		// 
		coverPathLabel.Anchor = AnchorStyles.Left;
		coverPathLabel.AutoSize = true;
		coverPathLabel.Location = new Point(12, 336);
		coverPathLabel.Name = "coverPathLabel";
		coverPathLabel.Size = new Size(71, 15);
		coverPathLabel.TabIndex = 19;
		coverPathLabel.Text = "Cover path";
		// 
		// coverPathTextBox
		// 
		// coverPathPanel
		coverPathPanel.Controls.Add(coverPathTextBox);
		coverPathPanel.Controls.Add(browseCoverButton);
		coverPathPanel.Dock = DockStyle.Fill;
		coverPathPanel.Location = new Point(123, 329);
		coverPathPanel.Margin = new Padding(3, 4, 12, 4);
		coverPathPanel.Name = "coverPathPanel";
		coverPathPanel.Size = new Size(485, 26);
		coverPathPanel.TabIndex = 20;
		// 
		// coverPathTextBox
		// 
		coverPathTextBox.Location = new Point(0, 0);
		coverPathTextBox.Margin = new Padding(0);
		coverPathTextBox.Name = "coverPathTextBox";
		coverPathTextBox.Size = new Size(390, 23);
		coverPathTextBox.TabIndex = 0;
		coverPathTextBox.TextChanged += coverPathTextBox_TextChanged;
		// 
		// browseCoverButton
		// 
		browseCoverButton.Location = new Point(398, 0);
		browseCoverButton.Margin = new Padding(8, 0, 0, 0);
		browseCoverButton.Name = "browseCoverButton";
		browseCoverButton.Size = new Size(80, 23);
		browseCoverButton.TabIndex = 1;
		browseCoverButton.Text = "Browse";
		browseCoverButton.UseVisualStyleBackColor = true;
		browseCoverButton.Click += browseCoverButton_Click;
		// 
		// coverPreviewBox
		// 
		coverPreviewBox.BorderStyle = BorderStyle.FixedSingle;
		coverPreviewBox.Location = new Point(123, 363);
		coverPreviewBox.Margin = new Padding(3, 4, 12, 4);
		coverPreviewBox.Name = "coverPreviewBox";
		coverPreviewBox.Size = new Size(96, 96);
		coverPreviewBox.SizeMode = PictureBoxSizeMode.Zoom;
		coverPreviewBox.TabIndex = 21;
		coverPreviewBox.TabStop = false;
		// 
		// buttonPanel
		// 
		buttonPanel.Controls.Add(saveButton);
		buttonPanel.Controls.Add(cancelButton);
		buttonPanel.Dock = DockStyle.Fill;
		buttonPanel.FlowDirection = FlowDirection.RightToLeft;
		buttonPanel.Location = new Point(123, 467);
		buttonPanel.Name = "buttonPanel";
		buttonPanel.Padding = new Padding(0, 10, 0, 0);
		buttonPanel.Size = new Size(485, 54);
		buttonPanel.TabIndex = 22;
		// 
		// saveButton
		// 
		saveButton.Location = new Point(394, 13);
		saveButton.Name = "saveButton";
		saveButton.Size = new Size(88, 29);
		saveButton.TabIndex = 0;
		saveButton.Text = "Save";
		saveButton.UseVisualStyleBackColor = true;
		saveButton.Click += saveButton_Click;
		// 
		// cancelButton
		// 
		cancelButton.DialogResult = DialogResult.Cancel;
		cancelButton.Location = new Point(300, 13);
		cancelButton.Name = "cancelButton";
		cancelButton.Size = new Size(88, 29);
		cancelButton.TabIndex = 1;
		cancelButton.Text = "Cancel";
		cancelButton.UseVisualStyleBackColor = true;
		// 
		// AddEditBookForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.Control;
		ClientSize = new Size(620, 524);
		Controls.Add(mainLayout);
		Font = new Font("Segoe UI", 9F);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimumSize = new Size(620, 560);
		Name = "AddEditBookForm";
		StartPosition = FormStartPosition.CenterParent;
		Text = "Add Book";
		mainLayout.ResumeLayout(false);
		mainLayout.PerformLayout();
		coverPathPanel.ResumeLayout(false);
		coverPathPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)coverPreviewBox).EndInit();
		yearPanel.ResumeLayout(false);
		yearPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)yearNumericUpDown).EndInit();
		buttonPanel.ResumeLayout(false);
		ResumeLayout(false);
	}
}
