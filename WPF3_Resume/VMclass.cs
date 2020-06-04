using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;


namespace WPF3_Resume
{
    //ViewModel Class
    class VMclass : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged class realization
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Private members
        private ObservableCollection<Resume> vmresumes;
        private readonly InfaceClass inface;//aggregated model reference
        private string name;
        private string surname;
        private string byfather;
        private string age;
        private string ismarryied;
        private string address;
        private string email;
        private string text;
        private bool csharp;
        private bool cplus;
        private bool java;
        private bool sql;
        private bool wpf;
        private bool winforms;
        private bool pattern;
        private bool english;
        private bool french;
        private bool deutch;
        private Resume _curResume;
        #endregion
        public VMclass()
        {
            vmresumes = new ObservableCollection<Resume>();
            inface = new InfaceClass();
            vmresumes = inface.AllResumes;
            _curResume = new Resume();
            ClearFields();           
        }


        #region Public properties
        public Resume CurrentResume { get { return _curResume; } set { if (_curResume != value) _curResume = value; SyncContentfromDropBox(); } }
        public ObservableCollection<Resume> GetvmResumes
        {
            get { return vmresumes; }
            set
            {
                vmresumes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(vmresumes)));
            }
        }
        public string Vm_Name  { get { return name; } set { if(name != value) name = value; inface.Name = Vm_Name; OnPropertyChanged(nameof(Vm_Name)); } }
        public string Vm_Surname { get { return surname; } set { if(surname != value) surname = value; inface.Surname = Vm_Surname; OnPropertyChanged(nameof(Vm_Surname)); } }
        public string Vm_byFather { get { return byfather; } set { if(byfather != value) byfather = value; inface.byFather = Vm_byFather; OnPropertyChanged(nameof(Vm_byFather)); } }
        public string Vm_Age { get { return age; } set { if (age != value) age = value; inface.Age = Vm_Age; OnPropertyChanged(nameof(Vm_Age)); } }
        public string Vm_isMarryied { get { return ismarryied; } set { if(ismarryied != value) ismarryied  = value; inface.isMarryied = Vm_isMarryied; OnPropertyChanged(nameof(Vm_isMarryied)); } }
        public string Vm_Address { get { return address; } set { if(address != value) address = value; inface.Address = Vm_Address; OnPropertyChanged(nameof(Vm_Address)); } }
        public string Vm_Email { get { return email; } set { if(email != value) email = value; inface.Email = Vm_Email; OnPropertyChanged(nameof(Vm_Email)); } }
        public string Vm_Text { get { return text; } set { if(text != value) text = value; inface.Text = Vm_Text; OnPropertyChanged(nameof(Vm_Text)); } }
        public bool Vm_isCsharp { get { return csharp; } set { csharp = value; inface.isCsharp = Vm_isCsharp; OnPropertyChanged(nameof(Vm_isCsharp)); } }
        public bool Vm_isJava { get { return java; } set { java = value; inface.isJava = Vm_isJava; OnPropertyChanged(nameof(Vm_isJava)); } }
        public bool Vm_isCplus { get { return cplus; } set { cplus = value; inface.isCplus = Vm_isCplus; OnPropertyChanged(nameof(Vm_isCplus)); } }
        public bool Vm_isSQL { get { return sql; } set { sql = value; inface.isSQL = Vm_isSQL; OnPropertyChanged(nameof(Vm_isSQL)); } }
        public bool Vm_isWPF { get { return wpf; } set { wpf = value; inface.isWPF = Vm_isWPF; OnPropertyChanged(nameof(Vm_isWPF)); } }
        public bool Vm_isWinForms { get { return winforms; } set { winforms = value; inface.isWinForms = Vm_isWinForms; OnPropertyChanged(nameof(Vm_isWinForms)); } }
        public bool Vm_isPatterns { get { return pattern; } set { pattern = value; inface.isPatterns = Vm_isPatterns; OnPropertyChanged(nameof(Vm_isPatterns)); } }
        public bool Vm_isEnglish { get { return english; } set { english = value; inface.isEnglish = Vm_isEnglish; OnPropertyChanged(nameof(Vm_isEnglish)); } }
        public bool Vm_isFrench { get { return french; } set { french = value; inface.isFrench = Vm_isFrench; OnPropertyChanged(nameof(Vm_isFrench)); } }
        public bool Vm_isDeutch { get { return deutch; } set { deutch = value; inface.isDeutch = Vm_isDeutch; OnPropertyChanged(nameof(Vm_isDeutch)); } }
        #endregion

        #region Commands
        private ICommand _Commandviewresume;
        public ICommand ButtonClickView//command view resume
        {
            get
            {
                if (_Commandviewresume == null)
                {
                    _Commandviewresume = new RelayCommand(param => GetnewWindow(), param => vmresumes.Count > 0);
                }
                return _Commandviewresume;
            }
        }


        private ICommand _Commandsaveresume;
        public ICommand ButtonClickOK//command save resume (button OK)
        {
            get
            {
                if (_Commandsaveresume == null)
                {
                    _Commandsaveresume = new RelayCommand(param => SaveResume(), param => CanSave());
                }
                return _Commandsaveresume;
            }
        }

        private ICommand _CommandClear;
        public ICommand ButtonClickCancel//command cancel resume (make clear all fields)
        {
            get
            {
                if (_CommandClear == null)
                {
                    _CommandClear = new RelayCommand(param => ClearFields(), param => CanClear());
                }
                return _CommandClear;
            }
        }

        private ICommand _CommandIgnore;
        public ICommand ButtonClickIgnore//command ignore
        {
            get
            {
                if (_CommandIgnore == null)
                {
                    _CommandIgnore = new RelayCommand(param => IgnoreResume(), param => CanIgnore());
                }
                return _CommandIgnore;
            }
        }
        #endregion

        #region Execute and CanExecute for Commands
        //CanExecute conditions for button cancel
        private bool CanClear()
        {
            if (Vm_Name.Length > 0 || Vm_Surname.Length > 0 || Vm_byFather.Length > 0 || Vm_Address.Length > 0 || Vm_Age.Length > 0
                || Vm_isMarryied.Length > 0 || Vm_Text.Length > 0 || Vm_Email.Length > 0 || Vm_isCplus || Vm_isCsharp || Vm_isJava || Vm_isSQL 
                || Vm_isWinForms || Vm_isWPF || Vm_isPatterns || Vm_isDeutch || Vm_isEnglish || Vm_isFrench)
                return true;
            else
                return false;
        }

        //CanExecute conditions for button OK
        private bool CanSave()
        {
            if (Vm_Name.Length > 0 && Vm_Surname.Length > 0 && Vm_byFather.Length > 0 && Vm_Address.Length > 0 && Vm_Age.Length > 0
                && Vm_Text.Length > 0 && ((Vm_isCplus || Vm_isCsharp || Vm_isJava || Vm_isSQL) && (Vm_isDeutch || Vm_isEnglish || Vm_isFrench)))
                return true;
            else
                return false;
        }

        //CanExecute conditions for button Ignore
        private bool CanIgnore()
        {
            if (vmresumes.Count > 0)
                return true;
            else
                return false;
        }

        //command show resume
        private void GetnewWindow()
        {
            Window1 window1 = new Window1();
            window1.DataContext = this;
            window1.ShowDialog();
        }

        //command ignore resume
        private void IgnoreResume()
        {
            inface.DeleteResume(CurrentResume);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentResume)));
        }

        //command save resume
        private void SaveResume()
        {
            inface.Addresume();
            ClearFields();
        }

        //command cancel clear
        private void ClearFields()
        {
            Vm_Name = System.String.Empty;
            Vm_Surname = System.String.Empty;
            Vm_byFather = System.String.Empty;
            Vm_isMarryied = System.String.Empty;
            Vm_Address = System.String.Empty;
            Vm_Age = System.String.Empty;
            Vm_Email = System.String.Empty;
            Vm_Text = System.String.Empty;
            Vm_isCsharp = false;
            Vm_isCplus = false;
            Vm_isJava = false;
            Vm_isSQL = false;
            Vm_isWinForms = false;
            Vm_isWPF = false;
            Vm_isPatterns = false;
            Vm_isDeutch = false;
            Vm_isEnglish = false;
            Vm_isFrench = false;           
        }

        //makes fill all fields from selected dropbox item
        private void SyncContentfromDropBox()
        {
            Vm_Name = _curResume.Name;
            Vm_Surname = _curResume.Surname;
            Vm_byFather = _curResume.byFather;
            Vm_Age = _curResume.Age;
            Vm_Address = _curResume.Address;
            Vm_isMarryied = _curResume.isMarryied;
            Vm_Email = _curResume.Email;
            Vm_Text = _curResume.Text;
            Vm_isCplus = _curResume.isCplus;
            Vm_isCsharp = _curResume.isCsharp;
            Vm_isJava = _curResume.isJava;
            Vm_isSQL = _curResume.isSQL;
            Vm_isWinForms = _curResume.isWinForms;
            Vm_isWPF = _curResume.isWPF;
            Vm_isPatterns = _curResume.isPatterns;
            Vm_isEnglish = _curResume.isEnglish;
            Vm_isFrench = _curResume.isFrench;
            Vm_isDeutch = _curResume.isDeutch;
        }
        #endregion
    }

}