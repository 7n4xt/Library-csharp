namespace LibraryManagement.Views;

partial class MainForm
{
	private System.ComponentModel.IContainer components = null;
	private Panel searchPanel = null!;
	private Label searchLabel = null!;
	private TextBox searchTextBox = null!;
	private Button searchButton = null!;
	private Button borrowButton = null!;
	private Button returnButton = null!;
	private Button addButton = null!;
	private Button deleteButton = null!;
	private Button themeToggleButton = null!;
	private DataGridView booksGrid = null!;
	private Label statusLabel = null!;

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
		searchPanel = new Panel();
		searchLabel = new Label();
		searchTextBox = new TextBox();
		searchButton = new Button();
		borrowButton = new Button();
		returnButton = new Button();
		addButton = new Button();
		deleteButton = new Button();
		themeToggleButton = new Button();
		booksGrid = new DataGridView();
		statusLabel = new Label();
		searchPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)booksGrid).BeginInit();
		SuspendLayout();
		// 
		// searchPanel
		// 
		searchPanel.Controls.Add(deleteButton);
		searchPanel.Controls.Add(themeToggleButton);
		searchPanel.Controls.Add(addButton);
		searchPanel.Controls.Add(returnButton);
		searchPanel.Controls.Add(borrowButton);
		searchPanel.Controls.Add(searchButton);
		searchPanel.Controls.Add(searchTextBox);
		searchPanel.Controls.Add(searchLabel);
		searchPanel.Dock = DockStyle.Top;
		searchPanel.Location = new Point(0, 0);
		searchPanel.Name = "searchPanel";
		searchPanel.Padding = new Padding(12, 10, 12, 10);
		searchPanel.Size = new Size(1100, 60);
		searchPanel.TabIndex = 0;
		// 
		// searchLabel
		// 
		searchLabel.AutoSize = true;
		searchLabel.Location = new Point(12, 18);
		searchLabel.Name = "searchLabel";
		searchLabel.Size = new Size(48, 15);
		searchLabel.TabIndex = 0;
		searchLabel.Text = "Search";
		// 
		// searchTextBox
		// 
		searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		searchTextBox.Location = new Point(72, 14);
		searchTextBox.Name = "searchTextBox";
		searchTextBox.PlaceholderText = "Titre, auteur, genre, ISBN";
		searchTextBox.Size = new Size(560, 23);
		searchTextBox.TabIndex = 1;
		searchTextBox.KeyDown += searchTextBox_KeyDown;
		// 
		// searchButton
		// 
		searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		searchButton.Location = new Point(638, 13);
		searchButton.Name = "searchButton";
		searchButton.Size = new Size(64, 26);
		searchButton.TabIndex = 2;
		searchButton.Text = "Search";
		searchButton.UseVisualStyleBackColor = true;
		searchButton.Click += searchButton_Click;
		// 
		// borrowButton
		// 
		borrowButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		borrowButton.Location = new Point(708, 13);
		borrowButton.Name = "borrowButton";
		borrowButton.Size = new Size(64, 26);
		borrowButton.TabIndex = 3;
		borrowButton.Text = "Borrow";
		borrowButton.UseVisualStyleBackColor = true;
		borrowButton.Click += borrowButton_Click;
		// 
		// returnButton
		// 
		returnButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		returnButton.Location = new Point(778, 13);
		returnButton.Name = "returnButton";
		returnButton.Size = new Size(64, 26);
		returnButton.TabIndex = 4;
		returnButton.Text = "Return";
		returnButton.UseVisualStyleBackColor = true;
		returnButton.Click += returnButton_Click;
		// 
		// addButton
		// 
		addButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		addButton.Location = new Point(848, 13);
		addButton.Name = "addButton";
		addButton.Size = new Size(64, 26);
		addButton.TabIndex = 5;
		addButton.Text = "Add";
		addButton.UseVisualStyleBackColor = true;
		addButton.Click += addButton_Click;
		// 
		// deleteButton
		// 
		deleteButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		deleteButton.Location = new Point(918, 13);
		deleteButton.Name = "deleteButton";
		deleteButton.Size = new Size(72, 26);
		deleteButton.TabIndex = 6;
		deleteButton.Text = "Delete";
		deleteButton.UseVisualStyleBackColor = true;
		deleteButton.Click += deleteButton_Click;
		// 
		// themeToggleButton
		// 
		themeToggleButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		themeToggleButton.Location = new Point(996, 13);
		themeToggleButton.Name = "themeToggleButton";
		themeToggleButton.Size = new Size(92, 26);
		themeToggleButton.TabIndex = 7;
		themeToggleButton.Text = "Dark";
		themeToggleButton.UseVisualStyleBackColor = true;
		themeToggleButton.Click += themeToggleButton_Click;
		// 
		// booksGrid
		// 
		booksGrid.AllowUserToAddRows = false;
		booksGrid.AllowUserToDeleteRows = false;
		booksGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		booksGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		booksGrid.Dock = DockStyle.Fill;
		booksGrid.Location = new Point(0, 60);
		booksGrid.Margin = new Padding(0);
		booksGrid.MultiSelect = false;
		booksGrid.Name = "booksGrid";
		booksGrid.ReadOnly = true;
		booksGrid.RowHeadersVisible = false;
		booksGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		booksGrid.Size = new Size(1100, 591);
		booksGrid.TabIndex = 1;
		booksGrid.CellDoubleClick += booksGrid_CellDoubleClick;
		// 
		// statusLabel
		// 
		statusLabel.AutoSize = true;
		statusLabel.Dock = DockStyle.Bottom;
		statusLabel.Location = new Point(0, 651);
		statusLabel.Margin = new Padding(8);
		statusLabel.Name = "statusLabel";
		statusLabel.Padding = new Padding(12, 8, 12, 8);
		statusLabel.Size = new Size(132, 31);
		statusLabel.TabIndex = 3;
		statusLabel.Text = "Loading books...";
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(1100, 682);
		Controls.Add(searchPanel);
		Controls.Add(booksGrid);
		Controls.Add(statusLabel);
		AcceptButton = searchButton;
		MinimumSize = new Size(900, 600);
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Library Management";
		searchPanel.ResumeLayout(false);
		searchPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)booksGrid).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
