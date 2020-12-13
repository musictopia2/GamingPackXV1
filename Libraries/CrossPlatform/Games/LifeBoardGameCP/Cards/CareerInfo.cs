using LifeBoardGameCP.Data;
namespace LifeBoardGameCP.Cards
{
    public class CareerInfo : LifeBaseCard
    {
        public CareerInfo()
        {
            CardCategory = EnumCardCategory.Career;
        }
        private EnumCareerType _career;
        public EnumCareerType Career
        {
            get { return _career; }
            set
            {
                if (SetProperty(ref _career, value))
                {
                    
                }
            }
        }
        private string _title = "";
        public string Title
        {
            get { return _title; }
            set
            {
                if (SetProperty(ref _title, value))
                {
                    
                }
            }
        }
        private EnumPayScale _scale1;
        public EnumPayScale Scale1
        {
            get { return _scale1; }
            set
            {
                if (SetProperty(ref _scale1, value))
                {
                    
                }
            }
        }
        private EnumPayScale _scale2;
        public EnumPayScale Scale2
        {
            get { return _scale2; }
            set
            {
                if (SetProperty(ref _scale2, value))
                {
                    
                }
            }
        }
        private bool _degreeRequired;
        public bool DegreeRequired
        {
            get { return _degreeRequired; }
            set
            {
                if (SetProperty(ref _degreeRequired, value))
                {
                    
                }
            }
        }
        private string _description = "";
        public string Description
        {
            get { return _description; }
            set
            {
                if (SetProperty(ref _description, value))
                {
                    
                }
            }
        }
    }
}