using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;

namespace WPF3_Resume
{
    
    //Model Class
    public class InfaceClass : INotifyPropertyChanged
    { 
        #region INotifyPropertyChanged Members

    /// <summary>
    /// Raises the PropertyChange event for the property specified
    /// </summary>
    /// <param name="propertyName">Property name to update. Is case-sensitive.</param>
    public virtual void RaisePropertyChanged(string propertyName)
    {
        OnPropertyChanged(propertyName);
    }

    /// <summary>
    /// Raised when a property on this object has a new value.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        #region Properties
        public string Name { get; set; }
        public string Surname { get; set; }
        public string byFather { get; set; }
        public string Age { get; set; }
        public string isMarryied { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool isCsharp { get; set; }
        public bool isJava { get; set; }
        public bool isCplus { get; set; }
        public bool isSQL { get; set; }
        public bool isWPF { get; set; }
        public bool isWinForms { get; set; }
        public bool isPatterns { get; set; }
        public bool isEnglish { get; set; }
        public bool isFrench { get; set; }
        public bool isDeutch { get; set; }

        #endregion
        public InfaceClass()
        {
            Resumes = new ObservableCollection<Resume>();
            LoadResumes();
        }
        private ObservableCollection<Resume> Resumes;

        #region Public and Private Methods
        public void Addresume()
        {
            Resume item = new Resume();
            item.Name = Name;
            item.Surname = Surname;
            item.byFather = byFather;
            item.Age = Age;
            item.isMarryied = isMarryied;
            item.Address = Address;
            item.Email = Email;
            item.Text =Text;
            item.isCsharp = isCsharp;
            item.isCplus = isCplus;
            item.isJava = isJava;
            item.isSQL = isSQL;
            item.isWPF = isWPF;
            item.isWinForms = isWinForms;
            item.isPatterns = isPatterns;
            item.isEnglish = isEnglish;
            item.isFrench = isFrench;
            item.isDeutch = isDeutch;

            Resumes.Add(item);
            SaveResumes();

        }
       
        public void DeleteResume(Resume item)
        {
            Resumes.Remove(item);
            SaveResumes();
        }

        public ObservableCollection<Resume> AllResumes
        {
            get { return Resumes; }
            set { Resumes = value; }
        }

   
        private void SaveResumes()
        {
            FileStream resumesstream = new FileStream("../../resumes.xml", FileMode.Create);
            XmlSerializer xmlser = new XmlSerializer(typeof(ObservableCollection<Resume>));
            xmlser.Serialize(resumesstream, Resumes);
            resumesstream.Close();

        }

        private void LoadResumes()
        {
            if (File.Exists("../../resumes.xml"))
            {
                FileStream resumesstream = new FileStream("../../resumes.xml", FileMode.Open);
                XmlSerializer xmlser = new XmlSerializer(typeof(ObservableCollection<Resume>));
                Resumes = (ObservableCollection<Resume>)xmlser.Deserialize(resumesstream);
                resumesstream.Close();
            }

        }
        #endregion
    }


    //Container Class
    public class Resume
    {
        #region Constructor with initialization
        public Resume()
        {
            Name = System.String.Empty;
            Surname = System.String.Empty;
            byFather = System.String.Empty;
            Age = System.String.Empty;
            isMarryied = System.String.Empty;
            Address = System.String.Empty;
            Email = System.String.Empty;
            Text = System.String.Empty;
            
            isCsharp = false;
            isJava = false;
            isCplus = false;
            isSQL = false;
            isWPF = false;
            isWinForms = false;
            isPatterns = false;
            isEnglish = false;
            isFrench = false;
            isDeutch = false;
            
        }
        #endregion

        #region Method for ComboBox
        public override string ToString()
        {
            return Name.ToString() + " " + Surname.ToString();
        }

        #endregion
        #region Public Properties
        public string Name { get; set; }
        public string Surname { get; set; }
        public string byFather { get; set; }
        public string Age { get; set; }
        public string isMarryied { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public bool isCsharp { get; set; }
        public bool isJava { get; set; }
        public bool isCplus { get; set; }
        public bool isSQL { get; set; }
        public bool isWPF { get; set; }
        public bool isWinForms { get; set; }
        public bool isPatterns { get; set; }
        public bool isEnglish { get; set; }
        public bool isFrench { get; set; }
        public bool isDeutch { get; set; }
        #endregion
    }

}