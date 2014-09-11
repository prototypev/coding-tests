namespace KeyValuePair
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Windows.Forms;

    using KeyValuePair.Properties;
    using KeyValuePair.Services;

    /// <summary>
    /// Backing code for the main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields

        /// <summary>
        /// The key-value pair service.
        /// In a full-blown product, we would use dependency injection with an IoC container to resolve this reference.
        /// </summary>
        private readonly IKeyValuePairService _keyValuePairService = new KeyValuePairService();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="FormClosingEventArgs"/> instance containing the event data.
        /// </param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(Resources.Question_ExitApplication, Resources.Exit, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialogResult != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Populates the list.
        /// </summary>
        /// <param name="sortOrder">
        /// The sort order.
        /// </param>
        private void PopulateList(SortOrder sortOrder)
        {
            string[] previouslySelectedItems = this.listBoxNameValuePairs.SelectedItems
                                                   .OfType<string>()
                                                   .ToArray();

            ListBox.ObjectCollection items = this.listBoxNameValuePairs.Items;
            items.Clear();

            object[] pairs = this._keyValuePairService.GetAll(sortOrder)
                                 .Select(pair => string.Format("{0}={1}", pair.Key, pair.Value))
                                 .Cast<object>()
                                 .ToArray();

            if (pairs.Length > 0)
            {
                for (int i = 0; i < pairs.Length; i++)
                {
                    items.Add(pairs[i]);

                    // Select back previously selected items if available
                    this.listBoxNameValuePairs.SetSelected(i, previouslySelectedItems.Contains(pairs[i]));
                }

                // Allow sorting only if list is non-empty
                this.buttonSortName.Enabled = 
                    this.buttonSortValue.Enabled = true;
            }
            else
            {
                // Disable sorting and deleting if list is empty
                this.buttonSortName.Enabled = 
                    this.buttonSortValue.Enabled = 
                    this.buttonDelete.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonAdd control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                this._keyValuePairService.CreateOrUpdate(this.textBoxInput.Text);

                // Assumption: Don't need to remember last chosen sort order
                this.PopulateList(SortOrder.NONE);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonDelete control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            IEnumerable<string> selectedItems = this.listBoxNameValuePairs.SelectedItems.OfType<string>();
            this._keyValuePairService.Delete(selectedItems);

            // Assumption: Don't need to remember last chosen sort order
            this.PopulateList(SortOrder.NONE);
        }

        /// <summary>
        /// Handles the Click event of the buttonExit control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.EventArgs"/> instance containing the event data.
        /// </param>
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the buttonSaveXml control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void buttonSaveXml_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
                                        {
                                            DefaultExt = "xml", 
                                            Filter = Resources.SaveFile_Filter
                                        };

            DialogResult dialogResult = dialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this._keyValuePairService.ExportAsXml(dialog.FileName);

                MessageBox.Show(Resources.SaveSuccessful, Resources.SaveSuccessful, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonSortName control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void buttonSortName_Click(object sender, EventArgs e)
        {
            this.PopulateList(SortOrder.NAME);
        }

        /// <summary>
        /// Handles the Click event of the buttonSortValue control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void buttonSortValue_Click(object sender, EventArgs e)
        {
            this.PopulateList(SortOrder.VALUE);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listBoxNameValuePairs control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        private void listBoxNameValuePairs_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.buttonDelete.Enabled = this.listBoxNameValuePairs.SelectedIndices.Count != 0;
        }

        #endregion
    }
}