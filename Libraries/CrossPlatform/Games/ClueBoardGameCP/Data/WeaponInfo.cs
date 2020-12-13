using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Drawing;
namespace ClueBoardGameCP.Data
{
    public class WeaponInfo : ObservableObject
    {
        public string Name { get; set; } = "";
        public int Room { get; set; }
        private EnumWeaponList _weapon;
        public EnumWeaponList Weapon
        {
            get
            {
                return _weapon;
            }
            set
            {
                if (SetProperty(ref _weapon, value) == true)
                {
                }
            }
        }
        public SizeF DefaultSize
        {
            get
            {
                if (Weapon == EnumWeaponList.None)
                {
                    return new SizeF(55, 72);
                }
                switch (Weapon)
                {
                    case EnumWeaponList.Candlestick:
                        {
                            return new SizeF(25, 35);
                        }

                    case EnumWeaponList.Knife:
                        {
                            return new SizeF(25, 22);
                        }

                    case EnumWeaponList.LeadPipe:
                        {
                            return new SizeF(15, 37);
                        }

                    case EnumWeaponList.Revolver:
                        {
                            return new SizeF(25, 15);
                        }

                    case EnumWeaponList.Rope:
                        {
                            return new SizeF(25, 20);
                        }

                    case EnumWeaponList.Wrench:
                        {
                            return new SizeF(25, 28);
                        }

                    default:
                        {
                            throw new BasicBlankException("Not supported");
                        }
                }
            }
        }
    }
}