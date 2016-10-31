using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InlämningDatabas1
{
    public partial class Form1 : Form
    {
        List<Contact> contact = new List<Contact>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Previous content is now handled by LoadList button and is
            //moved to "LoadListofContacts"            
        }

        private void btnLoadList_Click(object sender, EventArgs e)
        {
            LoadListofContacts();
        }

        public void LoadListofContacts()//Loads list from db
        {
            lstbContacts.Items.Clear();
            contact.Clear();
            ClearText();

            using (var context = new Model1())
            {
                var Person = context.LinkedContact.Select(s => s);
                foreach (var item in Person)
                {
                    contact.Add(item);
                    lstbContacts.Items.Add(item.Name);
                }
            }
        }

        private void lstbContacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbContacts.SelectedItem != null)
            {
                txtName.Text = contact[lstbContacts.SelectedIndex].Name;
                txtAddress.Text = contact[lstbContacts.SelectedIndex].Address;
                txtPostalCode.Text = contact[lstbContacts.SelectedIndex].PostalCode;
                txtCity.Text = contact[lstbContacts.SelectedIndex].City;
                txtPhone.Text = contact[lstbContacts.SelectedIndex].Phone;
                txtEmail.Text = contact[lstbContacts.SelectedIndex].Email;
                dtpDOB.Value = contact[lstbContacts.SelectedIndex].DOB;
            }

            else //if (lstbContacts.SelectedItem == null)
            {
                ClearText();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lstbContacts.Items.Clear();

            if (txtSearch.Text != "")
            {
                using (var context = new Model1())
                {
                    string search = txtSearch.Text.ToLower();
                    var L2EQuery = (from p in context.LinkedContact
                                    where p.Name.ToLower().Contains(search) || p.Address.ToLower().Contains(search) || p.PostalCode.ToLower().Contains(search) || p.City.ToLower().Contains(search) || p.Phone.ToLower().Contains(search) || p.Email.ToLower().Contains(search)
                                    orderby p.Name

                                    select p).ToList();
                    contact.Clear();
                    contact = L2EQuery;

                    foreach (var item in contact)
                    {
                        lstbContacts.Items.Add(item.Name);
                    }
                }
            }
            else
            {
                LoadListofContacts();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                Contact NewSingleContact = new Contact();

                NewSingleContact.Name = txtName.Text;
                NewSingleContact.Address = txtAddress.Text;
                NewSingleContact.PostalCode = txtPostalCode.Text;
                NewSingleContact.City = txtCity.Text;
                NewSingleContact.Phone = txtPhone.Text;
                NewSingleContact.Email = txtEmail.Text;
                NewSingleContact.DOB = dtpDOB.Value;

                contact.Add(NewSingleContact);

                using (var dbCtx = new Model1())
                {
                    dbCtx.LinkedContact.Add(NewSingleContact);
                    dbCtx.SaveChanges();
                }

                LoadListofContacts();
                ClearText();
            }

            else
            {
                MessageBox.Show("Don't be foolish. You have to enter a name first!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lstbContacts.SelectedItem != null)
            {
                Contact NewSingleContact = contact[lstbContacts.SelectedIndex];

                NewSingleContact.Name = txtName.Text;
                NewSingleContact.Address = txtAddress.Text;
                NewSingleContact.PostalCode = txtPostalCode.Text;
                NewSingleContact.City = txtCity.Text;
                NewSingleContact.Phone = txtPhone.Text;
                NewSingleContact.Email = txtEmail.Text;
                NewSingleContact.DOB = dtpDOB.Value;

                lstbContacts.Items.Add(NewSingleContact.Name);
                //contact.Add(NewSingleContact);

                using (var dbCtx = new Model1())
                {
                    dbCtx.Entry(NewSingleContact).State = System.Data.Entity.EntityState.Modified;
                    dbCtx.SaveChanges();
                }

                LoadListofContacts();
                ClearText();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstbContacts.SelectedItem != null)
            {
                Contact RemoveContact = contact[lstbContacts.SelectedIndex];

                using (var m = new Model1())
                {
                    MessageBox.Show(RemoveContact.Name + " has been deleted from your contacts.");

                    m.Entry(RemoveContact).State = System.Data.Entity.EntityState.Deleted;
                    m.SaveChanges();
                }

                ClearText();                
            }
        }

        public void ClearText()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtPostalCode.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            dtpDOB.ResetText();
            txtSearch.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }
    }
}
