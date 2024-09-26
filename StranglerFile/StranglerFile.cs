using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropertyRentalSystem
{
    public partial class frmOwnerAdd : Form
    {
        public frmOwnerAdd()
        {
            InitializeComponent();
        }

        private void btnAddOwner_Click(object sender, EventArgs e)
        {
            // On CLick Validate Add Owner Details.

            // checks name fields
            if (txtFirstName.Text.Equals(""))
            {
                MessageBox.Show("First Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text.Equals(""))
            {
                MessageBox.Show("Last Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return;
            }
            //checks valid phone number:

            if (txtPhoneNumber.Text.Equals(""))
            {
                MessageBox.Show("Phone Number Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }

            // moved validation of Phone numbers to a public helper class.
            bool isValidPhone = validationFunctions.validPhoneNumber(txtPhoneNumber.Text);
            if (!isValidPhone)
            {
                MessageBox.Show("Phone Number Must Be Valid,\nLarger than 7 numbers and can only have one +, no spaces!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }


            // checks Valid email
            if (txtEmailAddress.Text.Equals(""))
            {
                MessageBox.Show("Email Address Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }

            // moved validation of Email to a public helper class.
            bool isValidEmail = validationFunctions.validEmailAddres(txtEmailAddress.Text);

            if (!isValidEmail)
            {
                MessageBox.Show("Email entered is not valid format. \nsmith@exmaple.ie is a valid format.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }

            // eircode validation:

            if (txtHomeEircode.Text.Equals(""))
            {
                MessageBox.Show("Eircode Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHomeEircode.Focus();
                return;
            }

            // moved validation of Eircode to a public helper class to make it more gloabl.
            bool isValidEircode = validationFunctions.validEircode(txtHomeEircode.Text);

            if (!isValidEircode)
            {
                MessageBox.Show("Eircode is Invalid, Please enter a valid Eircode", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHomeEircode.Focus();
                return;
            }

            // IBAN Validation:
            if (txtOwnerIban.Text.Equals(""))
            {
                MessageBox.Show("IBAN Must be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOwnerIban.Focus();
                return;
            }
            // moved validation of Iban to a public helper class
            bool isValidIban = validationFunctions.validIban(txtOwnerIban.Text);

            if (!isValidIban)
            {
                MessageBox.Show("Valid IBAN Must be entered!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOwnerIban.Focus();
                return;
            }


            // Set Owner status to 'A' for Active,
            // Assign Owner an Owner ID.

            // Save to Data Store once validated.
            // NOT DOING THIS!

            // display confirmation Message:
            MessageBox.Show("Owner Has Been Added to the Owners Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset UI
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmailAddress.Clear();
            txtHomeEircode.Clear();
            txtPhoneNumber.Clear();
            txtOwnerIban.Clear();
            //Reset focus to first name.
            txtFirstName.Focus();

        }

        // moved Valid Iban, Valid Email and Valid Eircode to Validation helper class.


    }
}

namespace PropertyRentalSystem
{
    public partial class frmOwnerUpdate : Form
    {
        public frmOwnerUpdate()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSurnameSRH.Text.Equals("Smith") || txtSurnameSRH.Text.Equals("smith"))
            {
                // Find matching owners with surname.
                // retrieves owners with matching surnames from owners data file:
                grdOwners.Rows.Add("John", "Smith", "0877777777", 123);
                grdOwners.Rows.Add("Sarah", "Smith", "0867777777", 103);
                grdOwners.Rows.Add("Mary", "Smith", "0857777777", 134);
                grdOwners.Rows.Add("Jason", "Smith", "0897777777", 79);


                //display owners surname search grid
                grdOwners.Visible = true;

                // Hiding Owner details if new search.
                grpOwner.Visible = false;
            }
            else
            {
                MessageBox.Show("The surname " + txtSurnameSRH.Text + " Was not found,\nPlease try another surname such as  'Smith' ", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurnameSRH.Clear();
                txtSurnameSRH.Focus();
                return;
            }

        }

        private void grdOwners_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Retrieve Full owner Details from File.
            // Place retrieved data into the update details UI.
            grdOwners.CurrentRow.Selected = true;
            txtFirstName.Text = (string)grdOwners.Rows[e.RowIndex].Cells["firstName"].Value;
            txtLastName.Text = (string)grdOwners.Rows[e.RowIndex].Cells["lastName"].Value;
            txtPhoneNumber.Text = (string)grdOwners.Rows[e.RowIndex].Cells["phone"].Value;

            // these values will be retrieved from the data store in the future however for now it is simply populated:
            txtEmailAddress.Text = "Smith123@example.ie";
            txtHomeEircode.Text = "V92FFFF";
            txtOwnerIban.Text = "AIBK123456789123456789";
            cboOwnerStatus.SelectedIndex = 0;


            // display owner details.
            grpOwner.Visible = true;

            // hiding surname search grid after selection:
            grdOwners.Visible = false;

        }

        private void frmOwnerUpdate_Load(object sender, EventArgs e)
        {
            // loading the possible Owner Status's :
            cboOwnerStatus.Items.Add(" Active - 'A' ");
            cboOwnerStatus.Items.Add(" Inactive - 'I' ");

        }

        private void btnUpdateOwnerDetails_Click(object sender, EventArgs e)
        {
            // on click validation is same as add owner but with the addition of the status field.

            // checks name fields
            if (txtFirstName.Text.Equals(""))
            {
                MessageBox.Show("First Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text.Equals(""))
            {
                MessageBox.Show("Last Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return;
            }
            //checks valid phone number:

            if (txtPhoneNumber.Text.Equals(""))
            {
                MessageBox.Show("Phone Number Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }

            // moved validation of Phone numbers to a public helper class.
            bool isValidPhone = validationFunctions.validPhoneNumber(txtPhoneNumber.Text);
            if (!isValidPhone)
            {
                MessageBox.Show("Phone Number Must Be Valid,\nLarger than 7 numbers and can only have one +, no spaces!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }


            // checks Valid email
            if (txtEmailAddress.Text.Equals(""))
            {
                MessageBox.Show("Email Address Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }

            // moved validation of Email to a public helper class.
            bool isValidEmail = validationFunctions.validEmailAddres(txtEmailAddress.Text);

            if (!isValidEmail)
            {
                MessageBox.Show("Email entered is not valid format. \nsmith@exmaple.ie is a valid format.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }

            // eircode validation:

            if (txtHomeEircode.Text.Equals(""))
            {
                MessageBox.Show("Eircode Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHomeEircode.Focus();
                return;
            }

            // moved validation of Eircode to a public helper class to make it more gloabl.
            bool isValidEircode = validationFunctions.validEircode(txtHomeEircode.Text);

            if (!isValidEircode)
            {
                MessageBox.Show("Eircode is Invalid, Please enter a valid Eircode", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHomeEircode.Focus();
                return;
            }

            // IBAN Validation:
            if (txtOwnerIban.Text.Equals(""))
            {
                MessageBox.Show("IBAN Must be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOwnerIban.Focus();
                return;
            }
            // moved validation of Iban to a public helper class
            bool isValidIban = validationFunctions.validIban(txtOwnerIban.Text);

            if (!isValidIban)
            {
                MessageBox.Show("Valid IBAN Must be entered! Please check IBAN again.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOwnerIban.Focus();
                return;
            }


            // Set Owner status to 'A' for Active,
            // Assign Owner an Owner ID.

            // Save to Data Store once validated.
            // NOT DOING THIS!

            // display confirmation Message:
            MessageBox.Show("Owner Details have been updated \nAnd Updated in the Owners Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset UI
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmailAddress.Clear();
            txtHomeEircode.Clear();
            txtPhoneNumber.Clear();
            txtOwnerIban.Clear();
            cboOwnerStatus.SelectedIndex = -1;
            txtSurnameSRH.Clear();

            // Hide Update Details again.
            grpOwner.Visible = false;
            grdOwners.Visible = false;

            //Reset focus to Search field.
            txtSurnameSRH.Focus();

        }

    }
}



namespace PropertyRentalSystem
{
    public partial class frmPFRHome : Form
    {
        public frmPFRHome()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void mnuPFRAddPropertyType_Click(object sender, EventArgs e)
        {
            var addPropertyType = new frmPropertyTypeAdd();
            addPropertyType.Show();
        }

        private void mnuAddOwner_Click(object sender, EventArgs e)
        {
            var addOwner = new frmOwnerAdd();
            addOwner.Show();
        }

        private void mnuPFRAddProperty_Click(object sender, EventArgs e)
        {
            var addProperty = new frmPropertyAdd();
            addProperty.Show();
        }

        private void mnuAddTenant_Click(object sender, EventArgs e)
        {
            var addTenant = new frmTenantAdd();
            addTenant.Show();
        }

        private void mnuUpdateOwner_Click(object sender, EventArgs e)
        {
            var updateOwner = new frmOwnerUpdate();
            updateOwner.Show();
        }

        private void mnuPFRUpdatePropertyType_Click(object sender, EventArgs e)
        {
            var propertyTypeUpdate = new frmPropertyTypeUpdate();
            propertyTypeUpdate.Show();
        }

        private void mnuUpdateTenant_Click(object sender, EventArgs e)
        {
            var updateTenant = new frmTenantUpdate();
            updateTenant.Show();
        }

        private void mnuPFRUpdateProperty_Click(object sender, EventArgs e)
        {
            var updateProperty = new frmPropertyUpdate();
            updateProperty.Show();
        }

        private void mnuCreateRental_Click(object sender, EventArgs e)
        {
            var createRental = new frmRentalNew();
            createRental.Show();
        }

        private void mnuUpdateRental_Click(object sender, EventArgs e)
        {
            var updateRental = new frmRentalUpdate();
            updateRental.Show();
        }

        private void mnuRecordPayment_Click(object sender, EventArgs e)
        {
            var recordPayment = new frmRecordPayment();
            recordPayment.Show();
        }

        private void mnuYearlyCommission_Click(object sender, EventArgs e)
        {
            var yearlyCommissionReports = new frmYearlyCommission();
            yearlyCommissionReports.Show();
        }

        private void mnuRentalsInYear_Click(object sender, EventArgs e)
        {
            var rentalsYearReport = new frmRentalsYear();
            rentalsYearReport.Show();
        }
    }
}



namespace PropertyRentalSystem
{
    public partial class frmPropertyAdd : Form
    {
        public frmPropertyAdd()
        {
            InitializeComponent();
        }

        private void frmAddProperty_Load(object sender, EventArgs e)
        {

            cboPropertyType.Items.Add("BO - Bungalo");
            cboPropertyType.Items.Add("SD - Semi Detatched");
            cboPropertyType.Items.Add("DS - Standard Detatched");
            cboPropertyType.Items.Add("AP - Apartment");
            cboPropertyType.Items.Add("TH - Town House");

            cboHeatingSource.Items.Add("Oil Central Heating");
            cboHeatingSource.Items.Add("Electric Central Heating");
            cboHeatingSource.Items.Add("Heat Pump Central Heating");
            cboHeatingSource.Items.Add("Electric Radiators");
            cboHeatingSource.Items.Add("Storage Heaters");
            cboHeatingSource.Items.Add("Solid Fuel Stove");
        }

        private void btnAddProperty_Click(object sender, EventArgs e)
        {
            // When add property is pressed
            // Validate Data Entered

            // Owner not selected:
            if (txtPropertyOwner.Text.Equals(""))
            {
                MessageBox.Show("No Owner Selected.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyOwner.Focus();
                return;
            }

            // property type is empty:

            if (cboPropertyType.SelectedIndex == -1)
            {
                MessageBox.Show("Property Type must be Selected!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPropertyType.Focus();
                return;
            }

            // Property Name or Number validation.
            if (txtPropertyName.Text.Equals(""))
            {
                MessageBox.Show("Property Must have a name or number!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyName.Focus();
                return;
            }
            if (!validPropertyName(txtPropertyName.Text))
            {
                MessageBox.Show("Property Name Invalid!\nProperty name must be English letters, spaces and 's are allowed.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyName.Focus();
                return;
            }

            // Eircode Validation: (same as for Add Owner)

            if (txtEircode.Text.Equals(""))
            {
                MessageBox.Show("Eircode Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEircode.Focus();
                return;
            }
            // moved validation of numbers to a public helper class to make it more gloabl.
            bool isValidEircode = validationFunctions.validEircode(txtEircode.Text);

            if (!isValidEircode)
            {
                MessageBox.Show("Eircode is Invalid, Please enter a valid Eircode", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEircode.Focus();
                return;
            }

            // Monthly Rental Validation:

            if (txtMonthlyRent.Text.Equals(""))
            {
                MessageBox.Show("Montly Rent must be entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonthlyRent.Focus();
                return;
            }

            // moved validation of numbers to a public helper class to make it more gloabl.
            bool isValidNum = validationFunctions.validPositiveNumber(txtMonthlyRent.Text);
            if (!isValidNum)
            {
                MessageBox.Show("Invalid Monthly Rent\nMust be Positive Numeric value with only one decimal .", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonthlyRent.Focus();
                return;
            }


            // check if property Description is empty
            if (rtxPropertyDescription.Text.Equals(""))
            {
                MessageBox.Show("Property Description must be entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtxPropertyDescription.Focus();
                return;
            }


            // number up down does not allow negative values.

            //check if total rooms is more than 0
            if(numTotalRooms.Value == 0)
            {
                MessageBox.Show("A property must have atleast One Room!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numTotalRooms.Focus();
                return;
            }

            // check if total bedrooms is more than total rooms.
            if (numTotalRooms.Value < numTotalBedrooms.Value)
            {
                MessageBox.Show("Number of bedrooms cannot be greater than total rooms!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numTotalBedrooms.Focus();
                return;
            }
            // check if Ensuite bedrooms is more than total Bedrooms.
            if (numTotalBedrooms.Value < numEnsuiteBedrooms.Value)
            {
                MessageBox.Show("Number of Ensuite Bedrooms cannot be greater than Total Bedrooms!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numEnsuiteBedrooms.Focus();
                return;
            }

            // check if total ensuite bedrooms is more than total bathrooms.
            if (numTotalBathrooms.Value < numEnsuiteBedrooms.Value)
            {
                MessageBox.Show("Number of Ensuite Bedrooms cannot be greater than Total Bathrooms!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numEnsuiteBedrooms.Focus();
                return;
            }

            // check if total bedrooms + total bathrooms is more than total rooms.
            if (numTotalRooms.Value < (numTotalBedrooms.Value + numTotalBathrooms.Value))
            {
                MessageBox.Show("Number of bedrooms and bathrooms cannot be greater than total rooms!\nIncrease total rooms or rectify bedrooms/bathrooms", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numTotalRooms.Focus();
                return;
            }

            // check if heating source was selected.
            if (cboHeatingSource.SelectedIndex == -1)
            {
                MessageBox.Show("Heating Source must be Selected!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboHeatingSource.Focus();
                return;
            }



            // Give property Appropriate Property ID.
            // Set Property status to Available.
            // Save Property to Properties Data Store.
            // NOT DOING THIS!


            // Show confirmation message.
            MessageBox.Show("Property has been sucessfully added to the Properties Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // Reset UI.
            cboPropertyType.SelectedIndex = -1;
            cboHeatingSource.SelectedIndex = -1;
            txtPropertyName.Clear();
            txtEircode.Clear();
            txtMonthlyRent.Clear();
            rtxPropertyDescription.Clear();

            numTotalRooms.Value = 0;
            numTotalBedrooms.Value = 0;
            numTotalBathrooms.Value = 0;
            numEnsuiteBedrooms.Value = 0;
            numParkingSpaces.Value = 0;

            chkGardenSpace.Checked = false;
            chkHasWifi.Checked = false;
            chkOwnerOccupied.Checked = false;
            chkPetsAllowed.Checked = false;


            txtPropertyName.Focus();

        }

        private bool validPropertyName(string text)
        {
            bool result = true;
            Char[] nameChars = text.ToCharArray();


            for (int i = 0; i < nameChars.Length; i++)
            {
                // Allowed chars in property name are normal english letters &
                // spaces, numbers and 's.
                if (nameChars[i] == '\'' || nameChars[i] == ' '
                    || (nameChars[i] >= 'a' && nameChars[i] <= 'z') || (nameChars[i] >= 'A' && nameChars[i] <= 'Z')
                    || (nameChars[i] >= '0' && nameChars[i] <= '9'))
                {

                }
                else
                    result = false;
            }


            return result;
        }

        private void btnSurnameSRH_Click(object sender, EventArgs e)
        {
            if (txtSurnameSRH.Text.Equals("Smith") || txtSurnameSRH.Text.Equals("smith"))
            {

                // Find matching owners with surname.
                // retrieves owners with matching surnames from owners data file:
                grdOwners.Rows.Add("John", "Smith", "0877777777", 123);
                grdOwners.Rows.Add("Sarah", "Smith", "0867777777", 103);
                grdOwners.Rows.Add("Mary", "Smith", "0857777777", 134);
                grdOwners.Rows.Add("Jason", "Smith", "0897777777", 79);


                //display owners grid
                grdOwners.Visible = true;

                // Hide other grps if second time searching
                grpPropertyDetails.Visible = false;
                grpPropertyExtras.Visible = false;
                btnAddProperty.Visible = false;
            }
            else
            {
                MessageBox.Show("The surname " + txtSurnameSRH.Text + " Was not found,\nPlease try another surname such as  'Smith' ", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurnameSRH.Clear();
                txtSurnameSRH.Focus();
                return;
            }
        }

        private void grdOwners_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // Retrieve Full owner Details from File.
            grdOwners.CurrentRow.Selected = true;

            txtPropertyOwner.Text = (string)grdOwners.Rows[e.RowIndex].Cells["firstName"].Value + " " + (string)grdOwners.Rows[e.RowIndex].Cells["lastName"].Value;

            // display owner details.
            grpPropertyDetails.Visible = true;
            grpPropertyExtras.Visible = true;
            btnAddProperty.Visible = true;
            grdOwners.Visible = false;
            txtPropertyName.Focus();
        }
    }
}



namespace PropertyRentalSystem
{
    public partial class frmPropertyTypeAdd : Form
    {
        public frmPropertyTypeAdd()
        {
            InitializeComponent();
        }

        private void btnAddPropertyType_Click(object sender, EventArgs e)
        {

            // once add property type is clicked:

            // validation:
            // check for type code empty.
            if (txtPropertyTypeCode.Text.Equals(""))
            {
                MessageBox.Show("Property Type Code Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyTypeCode.Focus();
                return;
            }

            //check if code is 2 chars long
            if (txtPropertyTypeCode.Text.Length < 2 || txtPropertyTypeCode.Text.Length > 2)
            {
                MessageBox.Show("Property Type Code Must Be 2 characters long", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyTypeCode.Focus();
                return;
            }

            //Check to see if Code already Exists, (Using dummy data of SD representing Semi-Detatched.).

            if (txtPropertyTypeCode.Text.Equals("SD"))
            {
                MessageBox.Show("Property Type Code 'SD' Already Exists", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyTypeCode.Focus();
                return;
            }


            // check for type Description empty.
            if (txtPropertyTypeDescription.Text.Equals(""))
            {
                MessageBox.Show("Property Type Description Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyTypeDescription.Focus();
                return;
            }



            // Save to Data Store once validated.
            // NOT DOING THIS!

            // display confirmation Message:
            MessageBox.Show("Property Type Has Been Added", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // reset UI
            txtPropertyTypeCode.Clear();
            txtPropertyTypeDescription.Clear();
            txtPropertyTypeCode.Focus();
        }
    }
}


namespace PropertyRentalSystem
{
    public partial class frmPropertyTypeUpdate : Form
    {
        public frmPropertyTypeUpdate()
        {
            InitializeComponent();
        }

        private void frmPropertyTypeUpdate_Load(object sender, EventArgs e)
        {
            cboPropType.Items.Add("BO - Bungalo");
            cboPropType.Items.Add("DS - Standard Detatched");
            cboPropType.Items.Add("TH - Town House");
        }

        private void cboPropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPropType.SelectedIndex == -1)
            {
                txtPropertyTypeDescription.Clear();
                grpUpdateType.Visible = false;
            }
            if (cboPropType.SelectedIndex == 0)
            {
                txtPropertyTypeDescription.Text = "Single Story Detached house";
                grpUpdateType.Visible = true;
            }
            if (cboPropType.SelectedIndex == 1)
            {
                txtPropertyTypeDescription.Text = "Standard Detached house";
                grpUpdateType.Visible = true;
            }
            if (cboPropType.SelectedIndex == 2)
            {
                txtPropertyTypeDescription.Text = "Town House, central location";
                grpUpdateType.Visible = true;
            }



        }

        private void btnUpdatePropType_Click(object sender, EventArgs e)
        {
            // Once button clicked

            //check valid descritpion.

            // check for type Description empty.
            if (txtPropertyTypeDescription.Text.Equals(""))
            {
                MessageBox.Show("Property Type Description Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyTypeDescription.Focus();
                return;
            }



            // Update Data in Data Store once validated.
            // NOT DOING THIS!

            // display confirmation Message:
            MessageBox.Show("Property Type Has Been Updated", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // reset UI

            txtPropertyTypeDescription.Clear();
            grpUpdateType.Visible = false;
            cboPropType.SelectedIndex = -1;
        }
    }
}


namespace PropertyRentalSystem
{
    public partial class frmPropertyUpdate : Form
    {
        public frmPropertyUpdate()
        {
            InitializeComponent();
        }

        private void frmPropertyUpdate_Load(object sender, EventArgs e)
        {
            cboPropertyType.Items.Add("BO - Bungalo");
            cboPropertyType.Items.Add("SD - Semi Detatched");
            cboPropertyType.Items.Add("DS - Standard Detatched");
            cboPropertyType.Items.Add("AP - Apartment");
            cboPropertyType.Items.Add("TH - Town House");

            cboHeatingSource.Items.Add("Oil Central Heating");
            cboHeatingSource.Items.Add("Electric Central Heating");
            cboHeatingSource.Items.Add("Heat Pump Central Heating");
            cboHeatingSource.Items.Add("Electric Radiators");
            cboHeatingSource.Items.Add("Storage Heaters");
            cboHeatingSource.Items.Add("Solid Fuel Stove");
        }

        private void btnUpdateProperty_Click(object sender, EventArgs e)
        {
            // When add property is pressed
            // Validate Data Entered

            // Owner not selected:
            if (txtPropertyOwner.Text.Equals(""))
            {
                MessageBox.Show("No Owner Selected.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyOwner.Focus();
                return;
            }

            // property type is empty:

            if (cboPropertyType.SelectedIndex == -1)
            {
                MessageBox.Show("Property Type must be Selected!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPropertyType.Focus();
                return;
            }

            // Property Name or Number validation.
            if (txtPropertyName.Text.Equals(""))
            {
                MessageBox.Show("Property Must have a name or number!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyName.Focus();
                return;
            }
            if (!validPropertyName(txtPropertyName.Text))
            {
                MessageBox.Show("Property Name Invalid!\nProperty name must be English letters, spaces and 's are allowed.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPropertyName.Focus();
                return;
            }

            // Eircode Validation: (same as for Add Owner)

            if (txtEircode.Text.Equals(""))
            {
                MessageBox.Show("Eircode Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEircode.Focus();
                return;
            }

            // Monthly Rental Validation:

            if (txtMonthlyRent.Text.Equals(""))
            {
                MessageBox.Show("Montly Rent must be entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonthlyRent.Focus();
                return;
            }

            // moved validation of numbers to a public helper class to make it more gloabl.
            bool isValidNum = validationFunctions.validPositiveNumber(txtMonthlyRent.Text);
            if (!isValidNum)
            {
                MessageBox.Show("Invalid Monthly Rent\nMust be Positive Numeric value with only one decimal .", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonthlyRent.Focus();
                return;
            }


            // check if property Description is empty
            if (rtxPropertyDescription.Text.Equals(""))
            {
                MessageBox.Show("Property Description must be entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtxPropertyDescription.Focus();
                return;
            }


            // number up down does not allow negative values.

            //check if total rooms is more than 0
            if (numTotalRooms.Value == 0)
            {
                MessageBox.Show("A property must have atleast One Room!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numTotalRooms.Focus();
                return;
            }

            // check if total bedrooms is more than total rooms.
            if (numTotalRooms.Value < numTotalBedrooms.Value)
            {
                MessageBox.Show("Number of bedrooms cannot be greater than total rooms!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numTotalBedrooms.Focus();
                return;
            }
            // check if Ensuite bedrooms is more than total Bedrooms.
            if (numTotalBedrooms.Value < numEnsuiteBedrooms.Value)
            {
                MessageBox.Show("Number of Ensuite Bedrooms cannot be greater than Total Bedrooms!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numEnsuiteBedrooms.Focus();
                return;
            }

            // check if total ensuite bedrooms is more than total bathrooms.
            if (numTotalBathrooms.Value < numEnsuiteBedrooms.Value)
            {
                MessageBox.Show("Number of Ensuite Bedrooms cannot be greater than Total Bathrooms!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numEnsuiteBedrooms.Focus();
                return;
            }

            // check if total bedrooms + total bathrooms is more than total rooms.
            if (numTotalRooms.Value < (numTotalBedrooms.Value + numTotalBathrooms.Value))
            {
                MessageBox.Show("Number of bedrooms and bathrooms cannot be greater than total rooms!\nIncrease total rooms or rectify bedrooms/bathrooms", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numTotalRooms.Focus();
                return;
            }

            // check if heating source was selected.
            if (cboHeatingSource.SelectedIndex == -1)
            {
                MessageBox.Show("Heating Source must be Selected!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboHeatingSource.Focus();
                return;
            }



            // Give property Appropriate Property ID.
            // Set Property status to Available.
            // Save Property to Properties Data Store.
            // NOT DOING THIS!


            // Show confirmation message.
            MessageBox.Show("Property has been sucessfully Updated in the Properties Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);


            // Reset UI.
            cboPropertyType.SelectedIndex = -1;
            cboHeatingSource.SelectedIndex = -1;
            txtPropertyName.Clear();
            txtEircode.Clear();
            txtMonthlyRent.Clear();
            rtxPropertyDescription.Clear();
            txtPropertyOwner.Clear();
            txtSurnameSRH.Clear();

            numTotalRooms.Value = 0;
            numTotalBedrooms.Value = 0;
            numTotalBathrooms.Value = 0;
            numEnsuiteBedrooms.Value = 0;
            numParkingSpaces.Value = 0;

            chkGardenSpace.Checked = false;
            chkHasWifi.Checked = false;
            chkOwnerOccupied.Checked = false;
            chkPetsAllowed.Checked = false;

            grpPropertyDetails.Visible = false;
            grpPropertyExtras.Visible = false;
            btnUpdateProperty.Visible = false;


            txtPropertyName.Focus();
        }

        private bool validPropertyName(string text)
        {
            bool result = true;
            Char[] nameChars = text.ToCharArray();


            for (int i = 0; i < nameChars.Length; i++)
            {
                // Allowed chars in property name are normal english letters &
                // spaces, numbers and 's.
                if (nameChars[i] == '\'' || nameChars[i] == ' '
                    || (nameChars[i] >= 'a' && nameChars[i] <= 'z') || (nameChars[i] >= 'A' && nameChars[i] <= 'Z')
                    || (nameChars[i] >= '0' && nameChars[i] <= '9'))
                {

                }
                else
                    result = false;
            }


            return result;
        }

        private void btnSearchEircode_Click(object sender, EventArgs e)
        {
            // moved validation of numbers to a public helper class to make it more gloabl.
            bool isValidEircode = validationFunctions.validEircode(txtEircode.Text);

            if (!isValidEircode)
            {
                MessageBox.Show("Eircode is Invalid, Please enter a valid Eircode", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEircode.Clear();
                txtEircode.Focus();
                return;
            }
            else
            {
                if (txtEircode.Text.Equals("V92FFFF") || txtEircode.Text.Equals("v92ffff"))
                {
                    txtPropertyOwner.Text = "Adam O'Mahony";
                    cboPropertyType.SelectedIndex = 1;
                    cboHeatingSource.SelectedIndex = 5;
                    txtPropertyName.Text = "Birds Cottage";
                    txtMonthlyRent.Text = "500";
                    rtxPropertyDescription.Text = "Open plan country cottage bungalow set around 1 arce of woodlands.";
                    numTotalRooms.Value = 5;
                    numTotalBedrooms.Value = 2;
                    numTotalBathrooms.Value = 2;
                    numEnsuiteBedrooms.Value = 1;
                    numParkingSpaces.Value = 3;
                    chkGardenSpace.Checked = true;
                    chkHasWifi.Checked = true;
                    chkPetsAllowed.Checked = true;
                    chkOwnerOccupied.Checked = false;

                    grpPropertyDetails.Visible = true;
                    grpPropertyExtras.Visible = true;
                    btnUpdateProperty.Visible = true;


                }
                else
                {
                    MessageBox.Show("Eircode Not Found, Please try another Eircode Such as V92FFFF ", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEircode.Clear();
                    txtEircode.Focus();
                    return;
                }
            }
        }

        private void btnSurnameSRH_Click(object sender, EventArgs e)
        {
            if (txtSurnameSRH.Text.Equals("Smith") || txtSurnameSRH.Text.Equals("smith"))
            {
                // Find matching owners with surname.
                // retrieves owners with matching surnames from owners data file:
                grdOwners.Rows.Add("John", "Smith", "0877777777", 123);
                grdOwners.Rows.Add("Sarah", "Smith", "0867777777", 103);
                grdOwners.Rows.Add("Mary", "Smith", "0857777777", 134);
                grdOwners.Rows.Add("Jason", "Smith", "0897777777", 79);

                grdOwners.Visible = true;
                grpPropertyExtras.Visible = false;
                grpPropertyDetails.Visible = false;
                btnUpdateProperty.Visible = false;
            }
            else
            {
                MessageBox.Show("The surname " + txtSurnameSRH.Text + " Was not found,\nPlease try another surname such as  'Smith' ", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurnameSRH.Clear();
                txtSurnameSRH.Focus();
                return;
            }
        }

        private void grdOwners_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Retrieve Full owner Details from File.
            grdOwners.CurrentRow.Selected = true;

            txtPropertyOwner.Text = (string)grdOwners.Rows[e.RowIndex].Cells["firstName"].Value + " " + (string)grdOwners.Rows[e.RowIndex].Cells["lastName"].Value;

            // display owner details.
            grpPropertyDetails.Visible = true;
            grpPropertyExtras.Visible = true;
            btnUpdateProperty.Visible = true;
            grdOwners.Visible = false;
            txtPropertyName.Focus();
        }
    }
}


namespace PropertyRentalSystem
{
    public partial class frmRecordPayment : Form
    {
        public frmRecordPayment()
        {
            InitializeComponent();
        }

        private void btnSRHTenants_Click(object sender, EventArgs e)
        {
            //validate phone number entered.

            if (txtSRHTenant.Text.Equals(""))
            {
                MessageBox.Show("Phone Number Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }

            // moved validation of Phone numbers in helper class.
            bool isValidPhone = validationFunctions.validPhoneNumber(txtSRHTenant.Text);
            if (!isValidPhone)
            {
                MessageBox.Show("Phone Number Must Be Valid,\nLarger than 7 numbers and can only have one +, no spaces!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }

            // if valid phone retrieve data from the tenants file + the rental information from their active tenant Rental.
            if (txtSRHTenant.Text.Equals("0877777777"))
            {
                txtTenantName.Text = "Adam O'Mahony";
                txtMonthlyRent.Text = "500";
                txtActiveRental.Text = "Bird's Cottage";
                grpPayerDetails.Visible = true;
                grpPaymentDetails.Visible = true;
                btnRecordPayment.Visible = true;
            }
            else if (txtSRHTenant.Text.Equals("0879999999"))
            {
                txtTenantName.Text = "Billy El";
                txtMonthlyRent.Text = "400";
                txtActiveRental.Text = "57, Lee Accomodation";
                grpPayerDetails.Visible = true;
                grpPaymentDetails.Visible = true;
                btnRecordPayment.Visible = true;
            }
            else
            {
                MessageBox.Show("A tenant with that phone number was not found. \n Try 0877777777 or 0879999999 for example", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }
        }

        private void btnRecordPayment_Click(object sender, EventArgs e)
        {

            // Validate Data.

            // Date must be sometime in the futre.
            if (dtpPaymentDate.Value.Date > DateTime.Now)
            {
                MessageBox.Show("Payment Date must be from today or in the Past! cannot be in the Future.");
                return;
            }

            // Check is payment amount valid.
            if (txtPayAmount.Text.Equals(""))
            {
                MessageBox.Show("Payment Amount must be entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayAmount.Focus();
                return;
            }

            // moved validation of numbers to a public helper class to make it more gloabl.
            bool isValidNum = validationFunctions.validPositiveNumber(txtPayAmount.Text);
            if (!isValidNum)
            {
                MessageBox.Show("Invalid Payment Amount \nMust be Positive Numeric value with only one decimal .", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPayAmount.Focus();
                return;
            }


            // after all validation above has been checked,

            // Save Payment in the Payments Data file.
            // NOT DOING THIS!!!

            //show confirmation message
            MessageBox.Show("Payment Details Have Been Recorded in the Payments Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // reset UI.

            txtPayAmount.Clear();
            txtSRHTenant.Clear();
            txtMonthlyRent.Clear();
            txtTenantName.Clear();
            dtpPaymentDate.Value = DateTime.Now;

            grpPayerDetails.Visible = false;
            grpPaymentDetails.Visible = false;
            btnRecordPayment.Visible = false;

            txtSRHTenant.Focus();

        }
    }
}

namespace PropertyRentalSystem
{
    public partial class frmRentalNew : Form
    {
        public frmRentalNew()
        {
            InitializeComponent();
        }

        public Boolean tenant1Added = false;
        public Boolean tenant2Added = false;

        private void btnSRHEircode_Click(object sender, EventArgs e)
        {
            // moved validation of numbers to a public helper class to make it more gloabl.
            bool isValidEircode = validationFunctions.validEircode(txtEircodeSRH.Text);

            if (!isValidEircode)
            {
                MessageBox.Show("Eircode is Invalid, Please enter a valid Eircode", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEircodeSRH.Clear();
                txtEircodeSRH.Focus();
                return;
            }
            else
            {
                if (txtEircodeSRH.Text.Equals("V92FFFF") || txtEircodeSRH.Text.Equals("v92ffff"))
                {
                    txtPropertyOwner.Text = "Adam O'Mahony";
                    txtPropertyName.Text = "Birds Cottage";
                    txtPropertyEircode.Text = txtEircodeSRH.Text.ToUpper();
                    txtMonthlyRent.Text = "500";
                    grpPropertyDetails.Visible = true;
                    grpRentalDetails.Visible = true;
                    grpTenants.Visible = true;
                    btnCreateRental.Visible = true;

                }
                else
                {
                    MessageBox.Show("Property with that Eircode Not Found Or is Unavailable, Please try another Eircode Such as V92FFFF ", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEircodeSRH.Clear();
                    txtEircodeSRH.Focus();
                    return;
                }
            }
        }

        private void btnCreateRental_Click(object sender, EventArgs e)
        {

            // Validate Data.

            // Date must be sometime in the futre.
            if (dtpStartDate.Value.Date < DateTime.Now)
            {
                MessageBox.Show("Start Date must be from today or in the future! cannot be in the past.");
                return;
            }


            // direct debit must be set up before a rental contract can be made.
            // first months rent / deposit can be paid in cash though.
            if(chkDirectDebit.Checked == false)
            {
                MessageBox.Show("Please set up the direct debit for the tenants, this is an external process.");
                return;
            }

            // if no tenants are added.
            if(grdTenants.RowCount == 1)
            {
                MessageBox.Show("Please add atleast one tenant using the search bar.");
                txtSRHTenant.Focus();
                return;
            }

            // after all validation above has been checked,

            // Save Rental Contract to Rentals Data file.
            // Create Tenant Rentals in tenant rentals file for each tenant.
            // NOT DOING THIS!!!

            //show confirmation message
            MessageBox.Show("Rental Details Have Been Updated on the Rental Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // reset UI.
            txtEircodeSRH.Clear();
            txtMonthlyRent.Clear();
            txtPropertyEircode.Clear();
            txtPropertyName.Clear();
            txtPropertyOwner.Clear();

            dtpStartDate.Value = DateTime.Now;
            numRentDuration.Value = 12;
            chkDepositPaid.Checked = false;
            chkDirectDebit.Checked = false;

            txtSRHTenant.Clear();
            grdTenants.Rows.Clear();

            grpPropertyDetails.Visible = false;
            grpRentalDetails.Visible = false;
            grpTenants.Visible = false;
            btnCreateRental.Visible = false;

            txtEircodeSRH.Focus();
        }

        private void btnSRHTenants_Click(object sender, EventArgs e)
        {
            //validate phone number entered.

            if (txtSRHTenant.Text.Equals(""))
            {
                MessageBox.Show("Phone Number Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }

            // moved validation of Phone numbers in helper class.
            bool isValidPhone = validationFunctions.validPhoneNumber(txtSRHTenant.Text);
            if (!isValidPhone)
            {
                MessageBox.Show("Phone Number Must Be Valid,\nLarger than 7 numbers and can only have one +, no spaces!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }

            // if valid phone
            if (txtSRHTenant.Text.Equals("0877777777"))
            {
                if (tenant1Added == false){
                    grdTenants.Rows.Add("Adam", "Smith", "0877777777", 123);
                    grdTenants.Visible = true;
                    tenant1Added = true;
                }
                else
                {
                    MessageBox.Show("This Tenant was already added", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSRHTenant.Focus();
                    return;
                }
            }else if (txtSRHTenant.Text.Equals("0879999999"))
            {
                if (tenant2Added == false)
                {
                    grdTenants.Rows.Add("Helena", "Smith", "0877777777", 123);
                    grdTenants.Visible = true;
                    tenant2Added = true;
                }
                else
                {
                    MessageBox.Show("This Tenant was already added", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSRHTenant.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("A tenant with that phone number was not found. \n Try 0877777777 or 0879999999 for example", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }
        }
    }
}


namespace PropertyRentalSystem
{
    public partial class frmRentalsYear : Form
    {
        public frmRentalsYear()
        {
            InitializeComponent();
        }

        private void frmRentalsYear_Load(object sender, EventArgs e)
        {
            // On load retrieve data from completed years of opperation:
            // for example we are using 2018 and 2019 as exmaple data.
            cboRentalsInYear.Items.Add("Year 2018");
            cboRentalsInYear.Items.Add("Year 2019");
        }

        private void cboRentalsInYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            // example if year selected retrieve data from rentals for that year and produce graph:
            if (cboRentalsInYear.SelectedIndex == 0)
            {
                pboRentalsGraph.Image = Properties.Resources.Rentals2018;
                pboRentalsGraph.Refresh();
                pboRentalsGraph.Visible = true;
            }

            if (cboRentalsInYear.SelectedIndex == 1)
            {
                pboRentalsGraph.Image = Properties.Resources.Rentals2019;
                pboRentalsGraph.Refresh();
                pboRentalsGraph.Visible = true;
            }
        }
    }
}



namespace PropertyRentalSystem
{
    public partial class frmRentalUpdate : Form
    {
        public frmRentalUpdate()
        {
            InitializeComponent();
        }

        public Boolean tenant1Added = false;
        public Boolean tenant2Added = false;

        private void btnSRHEircode_Click(object sender, EventArgs e)
        {
            // moved validation of numbers to a public helper class to make it more gloabl.
            bool isValidEircode = validationFunctions.validEircode(txtEircodeSRH.Text);

            if (!isValidEircode)
            {
                MessageBox.Show("Eircode is Invalid, Please enter a valid Eircode", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEircodeSRH.Clear();
                txtEircodeSRH.Focus();
                return;
            }
            else
            {
                if (txtEircodeSRH.Text.Equals("V92FFFF") || txtEircodeSRH.Text.Equals("v92ffff"))
                {
                    txtPropertyOwner.Text = "Adam O'Mahony";
                    txtPropertyName.Text = "Birds Cottage";
                    txtPropertyEircode.Text = txtEircodeSRH.Text.ToUpper();
                    txtMonthlyRent.Text = "500";
                    grpPropertyDetails.Visible = true;
                    grpRentalDetails.Visible = true;
                    grpTenants.Visible = true;
                    btnUpdateRental.Visible = true;
                    dtpEndDate.Value = new DateTime(2023,11, 29);

                    chkDirectDebit.Checked = true;
                    chkDepositPaid.Checked = false;
                    grdTenants.Visible = true;

                    grdTenants.Rows.Add("John", "Smith", "0877777777", 123);
                    grdTenants.Rows.Add("Sarah", "Summers", "0867777777", 103);
                    grdTenants.Rows.Add("Mary", "Smith", "0857777777", 134);
                    grdTenants.Rows.Add("Jason", "Summers", "0897777777", 79);
                }
                else
                {
                    MessageBox.Show("Rental with that Eircode Not Found Or is Available for Rent, Please try another Eircode Such as V92FFFF ", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEircodeSRH.Clear();
                    txtEircodeSRH.Focus();
                    return;
                }
            }
        }

        private void grdTenants_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Retrieve Full owner Details from File.
            grdTenants.CurrentRow.Selected = true;

            DialogResult dialogResult = MessageBox.Show("Would you like to remove the tenant? " + (string)grdTenants.Rows[e.RowIndex].Cells["firstName"].Value + " from this rental?", "Warning!!!! Removing Tenant", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                // if yes Remove the tenant from the grd.
                grdTenants.Rows.RemoveAt(this.grdTenants.Rows[e.RowIndex].Index);
            }

        }

        private void btnSRHTenants_Click(object sender, EventArgs e)
        {
            // same code as new rental search.
            //validate phone number entered.

            if (txtSRHTenant.Text.Equals(""))
            {
                MessageBox.Show("Phone Number Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }

            // moved validation of Phone numbers in helper class.
            bool isValidPhone = validationFunctions.validPhoneNumber(txtSRHTenant.Text);
            if (!isValidPhone)
            {
                MessageBox.Show("Phone Number Must Be Valid,\nLarger than 7 numbers and can only have one +, no spaces!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }

            // if valid phone
            if (txtSRHTenant.Text.Equals("0877777777"))
            {
                if (tenant1Added == false)
                {
                    grdTenants.Rows.Add("Adam", "Smith", "0877777777", 123);
                    grdTenants.Visible = true;
                    tenant1Added = true;
                }
                else
                {
                    MessageBox.Show("This Tenant was already added", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSRHTenant.Focus();
                    return;
                }
            }
            else if (txtSRHTenant.Text.Equals("0879999999"))
            {
                if (tenant2Added == false)
                {
                    grdTenants.Rows.Add("Helena", "Smith", "0877777777", 123);
                    grdTenants.Visible = true;
                    tenant2Added = true;
                }
                else
                {
                    MessageBox.Show("This Tenant was already added", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSRHTenant.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("A tenant with that phone number was not found. \n Try 0877777777 or 0879999999 for example", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSRHTenant.Focus();
                return;
            }
        }

        private void btnUpdateRental_Click(object sender, EventArgs e)
        {
            //Validation for update:

            // extend months is always 0 or positive so no need for validation.

            // direct debit must be set up before a rental contract can be made.
            // first months rent / deposit can be paid in cash though.
            if (chkDirectDebit.Checked == false)
            {
                MessageBox.Show("Please set up the direct debit for the tenants, this is an external process.");
                return;
            }

            // if no tenants are added.
            if (grdTenants.RowCount == 1)
            {
                MessageBox.Show("Please add atleast one tenant using the search bar.");
                txtSRHTenant.Focus();
                return;
            }

            // after all validation above has been checked,

            // Update Rental Contract in the Rentals Data file.
            // Create Tenant Rentals in tenant rentals file for each new tenant, close for any deleted tenants.
            // NOT DOING THIS!!!

            //show confirmation message
            MessageBox.Show("Rental Details Have Been Updated in the Rental Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // reset UI.
            txtEircodeSRH.Clear();
            txtMonthlyRent.Clear();
            txtPropertyEircode.Clear();
            txtPropertyName.Clear();
            txtPropertyOwner.Clear();

            dtpEndDate.Value = DateTime.Now;
            numRentDuration.Value = 12;
            chkDepositPaid.Checked = false;
            chkDirectDebit.Checked = false;

            txtSRHTenant.Clear();
            grdTenants.Rows.Clear();

            grpPropertyDetails.Visible = false;
            grpRentalDetails.Visible = false;
            grpTenants.Visible = false;
            btnUpdateRental.Visible = false;

            txtEircodeSRH.Focus();
        }
    }
}


namespace PropertyRentalSystem
{
    public partial class frmTenantAdd : Form
    {
        public frmTenantAdd()
        {
            InitializeComponent();
        }

        private void btnAddTenant_Click(object sender, EventArgs e)
        {
            // On CLick Validate Add Tenant Details.
            // Very similar to add Owner however there is no Eircode.

            // checks name fields
            if (txtFirstName.Text.Equals(""))
            {
                MessageBox.Show("First Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text.Equals(""))
            {
                MessageBox.Show("Last Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return;
            }
            //checks valid phone number:

            if (txtPhoneNumber.Text.Equals(""))
            {
                MessageBox.Show("Phone Number Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }

            // moved validation of Phone numbers to a public helper class.
            bool isValidPhone = validationFunctions.validPhoneNumber(txtPhoneNumber.Text);
            if (!isValidPhone)
            {
                MessageBox.Show("Phone Number Must Be Valid,\nLarger than 7 numbers and can only have one +, no spaces!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }


            // checks Valid email
            if (txtEmailAddress.Text.Equals(""))
            {
                MessageBox.Show("Email Address Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }

            // moved validation of Email to a public helper class.
            bool isValidEmail = validationFunctions.validEmailAddres(txtEmailAddress.Text);

            if (!isValidEmail)
            {
                MessageBox.Show("Email entered is not valid", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }



            // IBAN Validation:
            if (txtTenantIban.Text.Equals(""))
            {
                MessageBox.Show("IBAN Must be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenantIban.Focus();
                return;
            }
            // moved validation of Iban to a public helper class
            bool isValidIban = validationFunctions.validIban(txtTenantIban.Text);

            if (!isValidIban)
            {
                MessageBox.Show("Valid IBAN Must be entered!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenantIban.Focus();
                return;
            }


            // Set Tenant status to 'A' for Active,
            // Assign Tenant an appropriate TenantID.
            // Save to Data Store once validated.
            // NOT DOING THIS!

            // display confirmation Message:
            MessageBox.Show("Tenant Has Been Added to the Tenants Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset UI
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmailAddress.Clear();
            txtPhoneNumber.Clear();
            txtTenantIban.Clear();
            //Reset focus to first name.
            txtFirstName.Focus();

        }
    }
}


namespace PropertyRentalSystem
{
    public partial class frmTenantUpdate : Form
    {
        public frmTenantUpdate()
        {
            InitializeComponent();
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (txtSurnameSRH.Text.Equals("Smith") || txtSurnameSRH.Text.Equals("smith"))
            {
                // Find matching owners with surname.
                // retrieves owners with matching surnames from owners data file:
                grdTenants.Rows.Add("John", "Smith", "0877777777", 123);
                grdTenants.Rows.Add("Sarah", "Smith", "0867777777", 103);
                grdTenants.Rows.Add("Mary", "Smith", "0857777777", 134);
                grdTenants.Rows.Add("Jason", "Smith", "0897777777", 79);


                //display owners surname search grid
                grdTenants.Visible = true;

                // Hiding Owner details if new search.
                grpTenant.Visible = false;
            }
            else
            {
                MessageBox.Show("The surname " + txtSurnameSRH.Text + " Was not found,\nPlease try another surname such as  'Smith' ", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurnameSRH.Clear();
                txtSurnameSRH.Focus();
                return;
            }
        }

        private void grdTenants_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Retrieve Full owner Details from File.
            // Place retrieved data into the update details UI.
            grdTenants.CurrentRow.Selected = true;
            txtFirstName.Text = (string)grdTenants.Rows[e.RowIndex].Cells["firstName"].Value;
            txtLastName.Text = (string)grdTenants.Rows[e.RowIndex].Cells["lastName"].Value;
            txtPhoneNumber.Text = (string)grdTenants.Rows[e.RowIndex].Cells["phone"].Value;

            // these values will be retrieved from the data store in the future however for now it is simply populated:
            txtEmailAddress.Text = "Smith123@example.ie";
            txtTenantIban.Text = "AIBK123456789123456789";
            cboTenantStatus.SelectedIndex = 0;


            // display owner details.
            grpTenant.Visible = true;

            // hiding surname search grid after selection:
            grdTenants.Visible = false;
        }



        private void frmTenantUpdate_Load(object sender, EventArgs e)
        {
            // loading the possible Tenant Status's :
            cboTenantStatus.Items.Add(" Active - 'A' ");
            cboTenantStatus.Items.Add(" Inactive - 'I' ");
        }

        private void btnUpdateTenantDetails_Click(object sender, EventArgs e)
        {
            // On CLick Validate Add Tenant Details.
            // Very similar to add Owner however there is no Eircode.

            // checks name fields
            if (txtFirstName.Text.Equals(""))
            {
                MessageBox.Show("First Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return;
            }
            if (txtLastName.Text.Equals(""))
            {
                MessageBox.Show("Last Name Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
                return;
            }
            //checks valid phone number:

            if (txtPhoneNumber.Text.Equals(""))
            {
                MessageBox.Show("Phone Number Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }

            // moved validation of Phone numbers to a public helper class.
            bool isValidPhone = validationFunctions.validPhoneNumber(txtPhoneNumber.Text);
            if (!isValidPhone)
            {
                MessageBox.Show("Phone Number Must Be Valid,\nLarger than 7 numbers and can only have one +, no spaces!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNumber.Focus();
                return;
            }


            // checks Valid email
            if (txtEmailAddress.Text.Equals(""))
            {
                MessageBox.Show("Email Address Must Be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }

            // moved validation of Email to a public helper class.
            bool isValidEmail = validationFunctions.validEmailAddres(txtEmailAddress.Text);

            if (!isValidEmail)
            {
                MessageBox.Show("Email entered is not valid", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailAddress.Focus();
                return;
            }



            // IBAN Validation:
            if (txtTenantIban.Text.Equals(""))
            {
                MessageBox.Show("IBAN Must be Entered", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenantIban.Focus();
                return;
            }
            // moved validation of Iban to a public helper class
            bool isValidIban = validationFunctions.validIban(txtTenantIban.Text);

            if (!isValidIban)
            {
                MessageBox.Show("Valid IBAN Must be entered!", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenantIban.Focus();
                return;
            }


            // Set Tenant status to 'A' for Active,
            // Assign Tenant an appropriate TenantID.
            // Save to Data Store once validated.
            // NOT DOING THIS!

            // display confirmation Message:
            MessageBox.Show("Tenant Details Have Been Updated on the Tenants Data Store", "Confirmation message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reset UI
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmailAddress.Clear();
            txtPhoneNumber.Clear();
            txtTenantIban.Clear();
            //Reset focus to first name.
            txtSurnameSRH.Clear();

            // Hide Update Details again.
            grpTenant.Visible = false;
            grdTenants.Visible = false;

            //Reset focus to Search field.
            txtSurnameSRH.Focus();
        }
    }
}



namespace PropertyRentalSystem
{
    public partial class frmYearlyCommission : Form
    {
        public frmYearlyCommission()
        {
            InitializeComponent();
        }

        private void frmYearlyCommission_Load(object sender, EventArgs e)
        {

            // On load retrieve data from completed years of opperation:
            // for example we are using 2018 and 2019 as exmaple data.
            cboYearlyCommision.Items.Add("Year 2018");
            cboYearlyCommision.Items.Add("Year 2019");
        }

        private void cboYearlyCommision_SelectedIndexChanged(object sender, EventArgs e)
        {
            // example if year selected retrieve data from rentals for that year and produce graph:
            if(cboYearlyCommision.SelectedIndex == 0)
            {
                pboYearlyGraph.Image = Properties.Resources.Comm2018;
                pboYearlyGraph.Refresh();
                pboYearlyGraph.Visible = true;
            }

            if(cboYearlyCommision.SelectedIndex == 1)
            {
                pboYearlyGraph.Image = Properties.Resources.Comm2019;
                pboYearlyGraph.Refresh();
                pboYearlyGraph.Visible = true;
            }
        }

    }
}

namespace PropertyRentalSystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPFRHome());
        }
    }
}


namespace PropertyRentalSystem
{
    public class validationFunctions
    {
        public static bool validPositiveNumber(string number)
        {
            bool result = true;
            char[] numberChars = number.ToCharArray();
            int dots = 0;

            // valid numbers must be numeric and only contain one '.' symbol - is not allowed.

            for (int i = 0; i < numberChars.Length; i++)
            {
                if (numberChars[i] == '.' || (numberChars[i] >= '0' && numberChars[i] <= '9'))
                {
                    if (numberChars[i] == '.')
                    {
                        if (dots == 1)
                        {
                            result = false;
                        }
                        else
                            dots++;

                    }
                }
                else
                    result = false;
            }

            return result;
        }

        public static bool validEircode(string eircode)
        {
            Boolean result = true;
            char[] eircodeChars = eircode.ToCharArray();

            if (eircode.Length != 7)
            {
                result = false;
                return result;
            }


            // checks if first value is a letter
            if ((eircodeChars[0] >= 'a' && eircodeChars[0] <= 'z') || (eircodeChars[0] >= 'A' && eircodeChars[0] <= 'Z'))
            {

            }
            else
            {
                result = false;
                return result;
            }


            // checks if following two are numbers:
            if ((eircodeChars[1] >= '0' && eircodeChars[1] <= '9') && (eircodeChars[2] >= '0' && eircodeChars[2] <= '9'))
            {

            }
            else
            {
                result = false;
                return result;
            }

            // check if last 4 chars are letters or numbers
            for (int i = 3; i < eircodeChars.Length; i++)
            {
                if ((eircodeChars[i] >= 'a' && eircodeChars[i] <= 'z') || (eircodeChars[i] >= 'A' && eircodeChars[i] <= 'Z') ||
                    (eircodeChars[i] >= '0' && eircodeChars[i] <= '9'))
                {

                }
                else
                {
                    result = false;
                    return result;
                }
            }

            // if result makes it to here and is still true will return true eircode is valid.
            return result;
        }

        public static bool validIban(string iban)
        {
            Boolean result = true;
            char[] ibanChars = iban.ToCharArray();

            // Iban's can vary between 22 and 34 characthers.
            // in Ireland the standard IBAN is 22 characthers in length.
            // there is other patterns involved but in the interest of simplicity
            // I am only adding simple validation. testing length and components.

            if (ibanChars.Length < 22 || ibanChars.Length > 34)
            {
                result = false;
            }


            // Iban must be composed of only numbers or letters:

            for (int i = 0; i < ibanChars.Length; i++)
            {
                if ((ibanChars[i] >= 'a' && ibanChars[i] <= 'z') || (ibanChars[i] >= 'A' && ibanChars[i] <= 'Z') ||
                    (ibanChars[i] >= '0' && ibanChars[i] <= '9'))
                {

                }
                else
                    result = false;
            }

            return result;
        }

        public static bool validPhoneNumber(string phone)
        {
            Boolean result = true;
            char[] phoneChars = phone.ToCharArray();
            int plusCount = 0;

            if (phoneChars.Length > 6)
            {
                for (int i = 0; i < phoneChars.Length; i++)
                {
                    if (phoneChars[i] >= '0' && phoneChars[i] <= '9')
                    {

                    }
                    else
                    {
                        if (phoneChars[i] == '+' && plusCount < 1)
                        {
                            plusCount++;
                        }
                        else
                        {
                            result = false;
                            return result;
                        }
                    }
                }
            }
            else
                result = false;



            return result;
        }

        public static bool validEmailAddres(String email)
        {
            Boolean result = true;
            int indexAt = email.IndexOf('@');
            int indexDot = email.IndexOf('.');
            int indexSpace = email.IndexOf(' ');
            char[] emailChars = email.ToCharArray();

            if (email.Equals("") || indexAt == -1 || indexDot == -1 || indexSpace != -1)
            {
                result = false;
            }

            // tests that the email char is valid allowed chars for email.
            // there is more but i am using a simple validation assuming standard emails.
            for (int i = 0; i < emailChars.Length; i++)
            {
                if ((emailChars[i] >= 'a' && emailChars[i] <= 'z') || (emailChars[i] >= 'A' && emailChars[i] <= 'Z')
                    || (emailChars[i] >= '0' && emailChars[i] <= '9') || emailChars[i] == '@' || emailChars[i] == '.' ||
                    emailChars[i] == '_' || emailChars[i] == '-')
                {

                }
                else
                    result = false;
            }
            return result;
        }
    }
}