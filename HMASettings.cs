using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.HMA
{
    public partial class HMASettings : UserControl
    {
        public bool AutoReset { get; set; }
        public bool AutoStart { get; set; }
        public bool _00Garden { get; set; }
        public bool _00Greenhouse { get; set; }
        public bool _00Mansion { get; set; }
        public bool _00MansionUpper { get; set; }
        public bool _01Chinatown { get; set; }
        public bool _02TerminusBottom { get; set; }
        public bool _02TerminusUpper { get; set; }
        public bool _03BurningHotel { get; set; }
        public bool _03Libary { get; set; }
        public bool _03PigeonCoop { get; set; }
        public bool _03ShangriLa { get; set; }
        public bool _03Trainstation { get; set; }        
        public bool _04Countryard { get; set; }
        public bool _04VixenClub { get; set; }
        public bool _04DressingRooms { get; set; }
        public bool _04DerelictBuilding { get; set; }
        public bool _04Conveniencestore { get; set; }
        public bool _04LoadingArea { get; set; }
        public bool _04ChineseNewYear { get; set; }
        public bool _06Victoria { get; set; }
        public bool _06Orphanage { get; set; }
        public bool _06CentralHeating { get; set; }
        public bool _07BallsOfFire { get; set; }
        public bool _08GunShop { get; set; }
        public bool _09StreetsOfHope { get; set; }
        public bool _09Barbershop { get; set; }
        public bool _10TheDesert { get; set; }
        public bool _11DeadEnd { get; set; }
        public bool _11OldMill { get; set; }
        public bool _11Descent { get; set; }
        public bool _11FactoryCompound { get; set; }        
        public bool _12TestFacility { get; set; }
        public bool _12Decontamination { get; set; }
        public bool _12RnD { get; set; }
        public bool _13PatriotsHangar { get; set; }
        public bool _13Arena { get; set; }
        public bool _14Parking { get; set; }
        public bool _14Reception { get; set; }
        public bool _14Cornfield { get; set; } 
        public bool _17Courthouse { get; set; }
        public bool _17HoldingCells { get; set; }
        public bool _17Prision { get; set; }  
        public bool _18CountyJail { get; set; }
        public bool _18Outgunned { get; set; }
        public bool _18Burn { get; set; } 
        public bool _18HopeFiar { get; set; } 
        public bool _21TailorShop { get; set; }  
        public bool _22BlackwaterPark { get; set; }  
        public bool _22Penthouse { get; set; }
        public bool _24BlackwaterRoof { get; set; } 
        public bool _25CementaryEntrence { get; set; } 
        public bool _25BurnwoodFamilyTomb { get; set; } 
        public bool _25Crematorium { get; set; } 

        private const bool DEFAULT_AUTORESET = true;
        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_00GARDEN = false;
        private const bool DEFAULT_00GREENHOUSE = true;
        private const bool DEFAULT_00MANSION = false;
        private const bool DEFAULT_00MANSIONUPPER = true;
        private const bool DEFAULT_01CHINATOWN = true;
        private const bool DEFAULT_02TERMINUSBOTTOM = false;
        private const bool DEFAULT_02TERMINUSUPPER = true;
        private const bool DEFAULT_03BURNINGHOTEL = false;
        private const bool DEFAULT_03LIBARY = false;
        private const bool DEFAULT_03PIGEONCOOP = false;
        private const bool DEFAULT_03SHANGRILA = false;
        private const bool DEFAULT_03TRAINSTATION = true;
        private const bool DEFAULT_04COUNTRYARD = false;
        private const bool DEFAULT_04VIXENCLUB = false;
        private const bool DEFAULT_04DRESSINGROOMS = false;
        private const bool DEFAULT_04DERELICTBUILDING = false;
        private const bool DEFAULT_04CONVINIENCESTORE = false;
        private const bool DEFAULT_04LOADINGAREA = false;
        private const bool DEFAULT_04CHINESENEWYEAR = true;
        private const bool DEFAULT_06VICTORIA = false;
        private const bool DEFAULT_06ORPHANAGE = false;
        private const bool DEFAULT_06CENTRALHEATING = true;
        private const bool DEFAULT_07BALLSOFFIRE = true;
        private const bool DEFAULT_08GUNSTORE = true;
        private const bool DEFAULT_09STREETSOFHOPE = false;
        private const bool DEFAULT_09BARBERSHOP = true;
        private const bool DEFAULT_10THEDESERT = true;
        private const bool DEFAULT_11DEADEND = false;
        private const bool DEFAULT_11OLDMILL = false;
        private const bool DEFAULT_11DESCENT = false;
        private const bool DEFAULT_11FACTORYCOMPOUND = true;
        private const bool DEFAULT_12TESTFACILITY = false;
        private const bool DEFAULT_12DECONTAMINATION = false;
        private const bool DEFAULT_12RND = true;
        private const bool DEFAULT_13PATRIOTSHANGAR = false;
        private const bool DEFAULT_13ARENA = true;
        private const bool DEFAULT_14PARKING = false;
        private const bool DEFAULT_14RECEPTION = false;
        private const bool DEFAULT_14CORNFIELD = true;
        private const bool DEFAULT_17COURTHOUSE = false;
        private const bool DEFAULT_17HOLDINGCELLS = false;
        private const bool DEFAULT_17PRISION = true;
        private const bool DEFAULT_18COUNTYJAIL= false;
        private const bool DEFAULT_18OUTGUNNED = false;
        private const bool DEFAULT_18BURN = false;
        private const bool DEFAULT_18HOPEFAIR = true;
        private const bool DEFAULT_21TAILORSHOP = false;
        private const bool DEFAULT_22BLACKWATERPARK = false;
        private const bool DEFAULT_22PENTHOUSE = true;
        private const bool DEFAULT_24BLACKWATERROOF = true;
        private const bool DEFAULT_25CEMENTARYENTRENCE = false;
        private const bool DEFAULT_25BURNWOODFAMILYTOMB = false;
        private const bool DEFAULT_25CREMATORIUM = true;

        public HMASettings()
        {
            InitializeComponent();

            this.chkAutoReset.DataBindings.Add("Checked", this, "AutoReset", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk00Garden.DataBindings.Add("Checked", this, "_00Garden", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk00Greenhouse.DataBindings.Add("Checked", this, "_00Greenhouse", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk00Mansion.DataBindings.Add("Checked", this, "_00Mansion", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk00MansionUpper.DataBindings.Add("Checked", this, "_00MansionUpper", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk01ChinatownSquare.DataBindings.Add("Checked", this, "_01Chinatown", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk02TerminusHotel.DataBindings.Add("Checked", this, "_02TerminusBottom", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk02UpperFloors.DataBindings.Add("Checked", this, "_02TerminusUpper", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk03BurningHotel.DataBindings.Add("Checked", this, "_03BurningHotel", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk03TheLibrary.DataBindings.Add("Checked", this, "_03Libary", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk03PigeonCoop.DataBindings.Add("Checked", this, "_03PigeonCoop", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk03Drugs.DataBindings.Add("Checked", this, "_03ShangriLa", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk03Station.DataBindings.Add("Checked", this, "_03Trainstation", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk04Courtyard.DataBindings.Add("Checked", this, "_04Countryard", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk04Club.DataBindings.Add("Checked", this, "_04VixenClub", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk04DressingRooms.DataBindings.Add("Checked", this, "_04DressingRooms", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk04DerelictBuilding.DataBindings.Add("Checked", this, "_04DerelictBuilding", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk04Store.DataBindings.Add("Checked", this, "_04Conveniencestore", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk04LoadingArea.DataBindings.Add("Checked", this, "_04LoadingArea", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk04ChineseNewYear.DataBindings.Add("Checked", this, "_04ChineseNewYear", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk06Victoria.DataBindings.Add("Checked", this, "_06Victoria", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk06Orphanage.DataBindings.Add("Checked", this, "_06Orphanage", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk06CentralHeating.DataBindings.Add("Checked", this, "_06CentralHeating", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk07Balls.DataBindings.Add("Checked", this, "_07BallsOfFire", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk08GunShop.DataBindings.Add("Checked", this, "_08GunShop", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk09_StretsOfHope.DataBindings.Add("Checked", this, "_09StreetsOfHope", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk09_Barbershop.DataBindings.Add("Checked", this, "_09Barbershop", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk10_Desert.DataBindings.Add("Checked", this, "_10TheDesert", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk11_DeadEnd.DataBindings.Add("Checked", this, "_11DeadEnd", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk11_OldMill.DataBindings.Add("Checked", this, "_11OldMill", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk11_Descent.DataBindings.Add("Checked", this, "_11Descent", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk11_FactoryComp.DataBindings.Add("Checked", this, "_11FactoryCompound", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk12_TestFacility.DataBindings.Add("Checked", this, "_12TestFacility", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk12_Decontamination.DataBindings.Add("Checked", this, "_12Decontamination", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk12_RnD.DataBindings.Add("Checked", this, "_12RnD", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk13_PatriotHangar.DataBindings.Add("Checked", this, "_13PatriotsHangar", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk13_Arena.DataBindings.Add("Checked", this, "_13Arena", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk14_Parking.DataBindings.Add("Checked", this, "_14Parking", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk14_Reception.DataBindings.Add("Checked", this, "_14Reception", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk14_Cornfield.DataBindings.Add("Checked", this, "_14Cornfield", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk17_Courthouse.DataBindings.Add("Checked", this, "_17Courthouse", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk17_HoldingCells.DataBindings.Add("Checked", this, "_17HoldingCells", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk17_Prision.DataBindings.Add("Checked", this, "_17Prision", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk18_CountyJail.DataBindings.Add("Checked", this, "_18CountyJail", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk18_Outgunned.DataBindings.Add("Checked", this, "_18Outgunned", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk18_Burn.DataBindings.Add("Checked", this, "_18Burn", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk18_HopeFair.DataBindings.Add("Checked", this, "_18HopeFiar", false, DataSourceUpdateMode.OnPropertyChanged);         
            this.chk21_TailorShop.DataBindings.Add("Checked", this, "_21TailorShop", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk22_BlackwaterPark.DataBindings.Add("Checked", this, "_22BlackwaterPark", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk22_Penthouse.DataBindings.Add("Checked", this, "_22Penthouse", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk24_Blackwaterroof.DataBindings.Add("Checked", this, "_24BlackwaterRoof", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk25_CementaryEntrance.DataBindings.Add("Checked", this, "_25CementaryEntrence", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk25_BurnwoodFamilyTomb.DataBindings.Add("Checked", this, "_25BurnwoodFamilyTomb", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chk25_Crematorium.DataBindings.Add("Checked", this, "_25Crematorium", false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.AutoReset = DEFAULT_AUTORESET;
            this.AutoStart = DEFAULT_AUTOSTART;
            this._00Garden = DEFAULT_00GARDEN;
            this._00Greenhouse = DEFAULT_00GREENHOUSE;
            this._00Mansion = DEFAULT_00MANSION;
            this._00MansionUpper = DEFAULT_00MANSIONUPPER;
            this._01Chinatown = DEFAULT_01CHINATOWN;
            this._02TerminusBottom = DEFAULT_02TERMINUSBOTTOM;
            this._02TerminusUpper = DEFAULT_02TERMINUSUPPER;
            this._03BurningHotel = DEFAULT_03BURNINGHOTEL;
            this._03Libary = DEFAULT_03LIBARY;
            this._03PigeonCoop = DEFAULT_03PIGEONCOOP;
            this._03ShangriLa = DEFAULT_03SHANGRILA;
            this._03Trainstation = DEFAULT_03TRAINSTATION;
            this._04Countryard = DEFAULT_04COUNTRYARD;
            this._04VixenClub = DEFAULT_04VIXENCLUB;
            this._04DressingRooms = DEFAULT_04DRESSINGROOMS;
            this._04DerelictBuilding = DEFAULT_04DERELICTBUILDING;
            this._04Conveniencestore = DEFAULT_04CONVINIENCESTORE;
            this._04LoadingArea = DEFAULT_04LOADINGAREA;
            this._04ChineseNewYear = DEFAULT_04CHINESENEWYEAR;
            this._06Victoria = DEFAULT_06VICTORIA;
            this._06Orphanage = DEFAULT_06ORPHANAGE;
            this._06CentralHeating = DEFAULT_06CENTRALHEATING;
            this._07BallsOfFire = DEFAULT_07BALLSOFFIRE;
            this._08GunShop = DEFAULT_08GUNSTORE;
            this._09StreetsOfHope = DEFAULT_09STREETSOFHOPE;
            this._09Barbershop = DEFAULT_09BARBERSHOP;
            this._10TheDesert = DEFAULT_10THEDESERT;
            this._11DeadEnd = DEFAULT_11DEADEND;
            this._11OldMill = DEFAULT_11OLDMILL;
            this._11Descent = DEFAULT_11DESCENT;
            this._11FactoryCompound = DEFAULT_11FACTORYCOMPOUND;
            this._12TestFacility = DEFAULT_12TESTFACILITY;
            this._12Decontamination = DEFAULT_12DECONTAMINATION;
            this._12RnD = DEFAULT_12RND;
            this._13PatriotsHangar = DEFAULT_13PATRIOTSHANGAR;
            this._13Arena = DEFAULT_13ARENA;
            this._14Parking = DEFAULT_14PARKING;
            this._14Reception = DEFAULT_14RECEPTION;
            this._14Cornfield = DEFAULT_14CORNFIELD;
            this._17Courthouse = DEFAULT_17COURTHOUSE;
            this._17HoldingCells = DEFAULT_17HOLDINGCELLS;
            this._17Prision = DEFAULT_17PRISION;
            this._18CountyJail = DEFAULT_18COUNTYJAIL;
            this._18Outgunned = DEFAULT_18OUTGUNNED;
            this._18Burn = DEFAULT_18BURN;
            this._18HopeFiar = DEFAULT_18HOPEFAIR;
            this._21TailorShop = DEFAULT_21TAILORSHOP;
            this._22BlackwaterPark = DEFAULT_22BLACKWATERPARK;
            this._22Penthouse = DEFAULT_22PENTHOUSE;
            this._24BlackwaterRoof = DEFAULT_24BLACKWATERROOF;
            this._25CementaryEntrence = DEFAULT_25CEMENTARYENTRENCE;
            this._25BurnwoodFamilyTomb = DEFAULT_25BURNWOODFAMILYTOMB;
            this._25Crematorium = DEFAULT_25CREMATORIUM;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(doc, "AutoReset", this.AutoReset));
            settingsNode.AppendChild(ToElement(doc, "AutoStart", this.AutoStart));
            settingsNode.AppendChild(ToElement(doc, "Garden", this._00Garden));
            settingsNode.AppendChild(ToElement(doc, "Greenhouse", this._00Greenhouse));
            settingsNode.AppendChild(ToElement(doc, "Mansion", this._00Mansion));
            settingsNode.AppendChild(ToElement(doc, "MansionUpper", this._00MansionUpper));
            settingsNode.AppendChild(ToElement(doc, "Chinatown", this._01Chinatown));
            settingsNode.AppendChild(ToElement(doc, "TerminusHotel", this._02TerminusBottom));
            settingsNode.AppendChild(ToElement(doc, "TerminusUpper", this._02TerminusUpper));
            settingsNode.AppendChild(ToElement(doc, "BurningHotel", this._03BurningHotel));
            settingsNode.AppendChild(ToElement(doc, "Libary", this._03Libary));
            settingsNode.AppendChild(ToElement(doc, "PigeonsCoop", this._03PigeonCoop));
            settingsNode.AppendChild(ToElement(doc, "DontDoDrugs", this._03ShangriLa));
            settingsNode.AppendChild(ToElement(doc, "ILikeTrains", this._03Trainstation));
            settingsNode.AppendChild(ToElement(doc, "Countryard", this._04Countryard));
            settingsNode.AppendChild(ToElement(doc, "VixenClub", this._04VixenClub));
            settingsNode.AppendChild(ToElement(doc, "DressingRooms", this._04DressingRooms));
            settingsNode.AppendChild(ToElement(doc, "DerelictBuilding", this._04DerelictBuilding));
            settingsNode.AppendChild(ToElement(doc, "ConvinienceStore", this._04Conveniencestore));
            settingsNode.AppendChild(ToElement(doc, "LoadingArea", this._04LoadingArea));
            settingsNode.AppendChild(ToElement(doc, "ChineseNewYear", this._04ChineseNewYear));
            settingsNode.AppendChild(ToElement(doc, "Victoria", this._06Victoria));
            settingsNode.AppendChild(ToElement(doc, "Orphanage", this._06Orphanage));
            settingsNode.AppendChild(ToElement(doc, "CentralHeating", this._06CentralHeating));
            settingsNode.AppendChild(ToElement(doc, "MyBallsAreOnFire", this._07BallsOfFire));
            settingsNode.AppendChild(ToElement(doc, "AndTheyShotMyBalls", this._08GunShop));
            settingsNode.AppendChild(ToElement(doc, "StreetsOfHope", this._09StreetsOfHope));
            settingsNode.AppendChild(ToElement(doc, "Barbershop", this._09Barbershop));
            settingsNode.AppendChild(ToElement(doc, "LennySucks", this._10TheDesert));
            settingsNode.AppendChild(ToElement(doc, "DeadEnd", this._11DeadEnd));
            settingsNode.AppendChild(ToElement(doc, "OldMill", this._11OldMill));
            settingsNode.AppendChild(ToElement(doc, "Descent", this._11Descent));
            settingsNode.AppendChild(ToElement(doc, "FactoryCompound", this._11FactoryCompound));
            settingsNode.AppendChild(ToElement(doc, "TestFacility", this._12TestFacility));
            settingsNode.AppendChild(ToElement(doc, "CleanYourAss", this._12Decontamination));
            settingsNode.AppendChild(ToElement(doc, "RnD", this._12RnD));
            settingsNode.AppendChild(ToElement(doc, "PatriotsHangar", this._13PatriotsHangar));
            settingsNode.AppendChild(ToElement(doc, "Arena", this._13Arena));
            settingsNode.AppendChild(ToElement(doc, "Level1WithBitches", this._14Parking));
            settingsNode.AppendChild(ToElement(doc, "Level2WithBitches", this._14Reception));
            settingsNode.AppendChild(ToElement(doc, "Level3WithBitches", this._14Cornfield));
            settingsNode.AppendChild(ToElement(doc, "Courthouse", this._17Courthouse));
            settingsNode.AppendChild(ToElement(doc, "HoldingCells", this._17HoldingCells));
            settingsNode.AppendChild(ToElement(doc, "Prision", this._17Prision));
            settingsNode.AppendChild(ToElement(doc, "WTF", this._18CountyJail));
            settingsNode.AppendChild(ToElement(doc, "Outgunned", this._18Outgunned));
            settingsNode.AppendChild(ToElement(doc, "BurnBabyBurn", this._18Burn));
            settingsNode.AppendChild(ToElement(doc, "HopeFair", this._18HopeFiar));
            settingsNode.AppendChild(ToElement(doc, "TailorShop", this._21TailorShop));
            settingsNode.AppendChild(ToElement(doc, "BlackwaterPark", this._22BlackwaterPark));
            settingsNode.AppendChild(ToElement(doc, "Penthouse", this._22Penthouse));
            settingsNode.AppendChild(ToElement(doc, "BlackwaterRoof", this._24BlackwaterRoof));
            settingsNode.AppendChild(ToElement(doc, "CementaryEntrance", this._25CementaryEntrence));
            settingsNode.AppendChild(ToElement(doc, "BurnwoodTomb", this._25BurnwoodFamilyTomb));
            settingsNode.AppendChild(ToElement(doc, "Crematorium", this._25Crematorium));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.AutoReset = ParseBool(settings, "AutoReset", DEFAULT_AUTORESET);
            this.AutoStart = ParseBool(settings, "AutoStart", DEFAULT_AUTOSTART);
            this._00Garden = ParseBool(settings, "Garden", DEFAULT_00GARDEN);
            this._00Greenhouse = ParseBool(settings, "Greenhouse", DEFAULT_00GREENHOUSE);
            this._00Mansion = ParseBool(settings, "Mansion", DEFAULT_00MANSION);
            this._00MansionUpper = ParseBool(settings, "MansionUpper", DEFAULT_00MANSIONUPPER);
            this._01Chinatown = ParseBool(settings, "Chinatown", DEFAULT_01CHINATOWN);
            this._02TerminusBottom = ParseBool(settings, "TerminusHotel", DEFAULT_02TERMINUSBOTTOM);
            this._02TerminusUpper = ParseBool(settings, "TerminusUpper", DEFAULT_02TERMINUSUPPER);
            this._03BurningHotel = ParseBool(settings, "BurningHotel", DEFAULT_03BURNINGHOTEL);
            this._03Libary = ParseBool(settings, "Libary", DEFAULT_03LIBARY);
            this._03PigeonCoop = ParseBool(settings, "PigeonsCoop", DEFAULT_03PIGEONCOOP);
            this._03ShangriLa = ParseBool(settings, "DontDoDrugs", DEFAULT_03SHANGRILA);
            this._03Trainstation = ParseBool(settings, "ILikeTrains", DEFAULT_03TRAINSTATION);
            this._04Countryard = ParseBool(settings, "Countryard", DEFAULT_04COUNTRYARD);
            this._04VixenClub = ParseBool(settings, "VixenClub", DEFAULT_04VIXENCLUB);
            this._04DressingRooms = ParseBool(settings, "DressingRooms", DEFAULT_04DRESSINGROOMS);
            this._04DerelictBuilding = ParseBool(settings, "DerelictBuilding", DEFAULT_04DERELICTBUILDING);
            this._04Conveniencestore = ParseBool(settings, "ConvinienceStore", DEFAULT_04CONVINIENCESTORE);
            this._04LoadingArea = ParseBool(settings, "LoadingArea", DEFAULT_04LOADINGAREA);
            this._04ChineseNewYear = ParseBool(settings, "ChineseNewYear", DEFAULT_04CHINESENEWYEAR);
            this._06Victoria = ParseBool(settings, "Victoria", DEFAULT_06VICTORIA);
            this._06Orphanage = ParseBool(settings, "Orphanage", DEFAULT_06ORPHANAGE);
            this._06CentralHeating = ParseBool(settings, "CentralHeating", DEFAULT_06CENTRALHEATING);
            this._07BallsOfFire = ParseBool(settings, "MyBallsAreOnFire", DEFAULT_07BALLSOFFIRE);
            this._08GunShop = ParseBool(settings, "AndTheyShotMyBalls", DEFAULT_08GUNSTORE);
            this._09StreetsOfHope = ParseBool(settings, "StreetsOfHope", DEFAULT_09STREETSOFHOPE);
            this._09Barbershop = ParseBool(settings, "Barbershop", DEFAULT_09BARBERSHOP);
            this._10TheDesert = ParseBool(settings, "LennySucks", DEFAULT_10THEDESERT);
            this._11DeadEnd = ParseBool(settings, "DeadEnd", DEFAULT_11DEADEND);
            this._11OldMill = ParseBool(settings, "OldMill", DEFAULT_11OLDMILL);
            this._11Descent = ParseBool(settings, "Descent", DEFAULT_11DESCENT);
            this._11FactoryCompound = ParseBool(settings, "FactoryCompound", DEFAULT_11FACTORYCOMPOUND);
            this._12TestFacility = ParseBool(settings, "TestFacility", DEFAULT_12TESTFACILITY);
            this._12Decontamination = ParseBool(settings, "CleanYourAss", DEFAULT_12DECONTAMINATION);
            this._12RnD = ParseBool(settings, "RnD", DEFAULT_12RND);
            this._13PatriotsHangar = ParseBool(settings, "PatriotsHangar", DEFAULT_13PATRIOTSHANGAR);
            this._13Arena = ParseBool(settings, "Arena", DEFAULT_13ARENA);
            this._14Parking = ParseBool(settings, "Level1WithBitches", DEFAULT_14PARKING);
            this._14Reception = ParseBool(settings, "Level2WithBitches", DEFAULT_14RECEPTION);
            this._14Cornfield = ParseBool(settings, "Level3WithBitches", DEFAULT_14CORNFIELD);
            this._17Courthouse = ParseBool(settings, "Courthouse", DEFAULT_17COURTHOUSE);
            this._17HoldingCells = ParseBool(settings, "HoldingCells", DEFAULT_17HOLDINGCELLS);
            this._17Prision = ParseBool(settings, "Prision", DEFAULT_17PRISION);
            this._18CountyJail = ParseBool(settings, "WTF", DEFAULT_18COUNTYJAIL);
            this._18Outgunned = ParseBool(settings, "Outgunned", DEFAULT_18OUTGUNNED);
            this._18Burn = ParseBool(settings, "BurnBabyBurn", DEFAULT_18BURN);
            this._18HopeFiar = ParseBool(settings, "HopeFair", DEFAULT_18HOPEFAIR);
            this._21TailorShop = ParseBool(settings, "TailorShop", DEFAULT_21TAILORSHOP);
            this._22BlackwaterPark = ParseBool(settings, "BlackwaterPark", DEFAULT_22BLACKWATERPARK);
            this._22Penthouse = ParseBool(settings, "Penthouse", DEFAULT_22PENTHOUSE);
            this._24BlackwaterRoof = ParseBool(settings, "BlackwaterRoof", DEFAULT_24BLACKWATERROOF);
            this._25CementaryEntrence = ParseBool(settings, "CementaryEntrance", DEFAULT_25CEMENTARYENTRENCE);
            this._25BurnwoodFamilyTomb = ParseBool(settings, "BurnwoodTomb", DEFAULT_25BURNWOODFAMILYTOMB);
            this._25Crematorium = ParseBool(settings, "Crematorium", DEFAULT_25CREMATORIUM);
        }

        static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
            bool val;
            return settings[setting] != null ?
                (Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_)
                : default_;
        }

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }

        private void radialPresetAllSections_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAutoReset.Checked = true;
            this.chkAutoStart.Checked = true;
            this.chk00Garden.Checked = true;
            this.chk00Greenhouse.Checked = true;
            this.chk00Mansion.Checked = true;
            this.chk00MansionUpper.Checked = true;
            this.chk01ChinatownSquare.Checked = true;
            this.chk02TerminusHotel.Checked = true;
            this.chk02UpperFloors.Checked = true;
            this.chk03BurningHotel.Checked = true;
            this.chk03TheLibrary.Checked = true;
            this.chk03PigeonCoop.Checked = true;
            this.chk03Drugs.Checked = true;
            this.chk03Station.Checked = true;
            this.chk04Courtyard.Checked = true;
            this.chk04Club.Checked = true;
            this.chk04DressingRooms.Checked = true;
            this.chk04DerelictBuilding.Checked = true;
            this.chk04Store.Checked = true;
            this.chk04LoadingArea.Checked = true;
            this.chk04ChineseNewYear.Checked = true;
            this.chk06Victoria.Checked = true;
            this.chk06Orphanage.Checked = true;
            this.chk06CentralHeating.Checked = true;
            this.chk07Balls.Checked = true;
            this.chk08GunShop.Checked = true;
            this.chk09_StretsOfHope.Checked = true;
            this.chk09_Barbershop.Checked = true;
            this.chk10_Desert.Checked = true;
            this.chk11_DeadEnd.Checked = true;
            this.chk11_OldMill.Checked = true;
            this.chk11_Descent.Checked = true;
            this.chk11_FactoryComp.Checked = true;
            this.chk12_TestFacility.Checked = true;
            this.chk12_Decontamination.Checked = true;
            this.chk12_RnD.Checked = true;
            this.chk13_PatriotHangar.Checked = true;
            this.chk13_Arena.Checked = true;
            this.chk14_Parking.Checked = true;
            this.chk14_Reception.Checked = true;
            this.chk14_Cornfield.Checked = true;
            this.chk17_Courthouse.Checked = true;
            this.chk17_HoldingCells.Checked = true;
            this.chk17_Prision.Checked = true;
            this.chk18_CountyJail.Checked = true;
            this.chk18_Outgunned.Checked = true;
            this.chk18_Burn.Checked = true;
            this.chk18_HopeFair.Checked = true;
            this.chk21_TailorShop.Checked = true;
            this.chk22_BlackwaterPark.Checked = true;
            this.chk22_Penthouse.Checked = true;
            this.chk24_Blackwaterroof.Checked = true;
            this.chk25_CementaryEntrance.Checked = true;
            this.chk25_BurnwoodFamilyTomb.Checked = true;
            this.chk25_Crematorium.Checked = true;
        }

        private void radialPresetOnlyLoadRemoving_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAutoReset.Checked = true;
            this.chkAutoStart.Checked = true;
            this.chk00Garden.Checked = false;
            this.chk00Greenhouse.Checked = false;
            this.chk00Mansion.Checked = false;
            this.chk00MansionUpper.Checked = false;
            this.chk01ChinatownSquare.Checked = false;
            this.chk02TerminusHotel.Checked = false;
            this.chk02UpperFloors.Checked = false;
            this.chk03BurningHotel.Checked = false;
            this.chk03TheLibrary.Checked = false;
            this.chk03PigeonCoop.Checked = false;
            this.chk03Drugs.Checked = false;
            this.chk03Station.Checked = false;
            this.chk04Courtyard.Checked = false;
            this.chk04Club.Checked = false;
            this.chk04DressingRooms.Checked = false;
            this.chk04DerelictBuilding.Checked = false;
            this.chk04Store.Checked = false;
            this.chk04LoadingArea.Checked = false;
            this.chk04ChineseNewYear.Checked = false;
            this.chk06Victoria.Checked = false;
            this.chk06Orphanage.Checked = false;
            this.chk06CentralHeating.Checked = false;
            this.chk07Balls.Checked = false;
            this.chk08GunShop.Checked = false;
            this.chk09_StretsOfHope.Checked = false;
            this.chk09_Barbershop.Checked = false;
            this.chk10_Desert.Checked = false;
            this.chk11_DeadEnd.Checked = false;
            this.chk11_OldMill.Checked = false;
            this.chk11_Descent.Checked = false;
            this.chk11_FactoryComp.Checked = false;
            this.chk12_TestFacility.Checked = false;
            this.chk12_Decontamination.Checked = false;
            this.chk12_RnD.Checked = false;
            this.chk13_PatriotHangar.Checked = false;
            this.chk13_Arena.Checked = false;
            this.chk14_Parking.Checked = false;
            this.chk14_Reception.Checked = false;
            this.chk14_Cornfield.Checked = false;
            this.chk17_Courthouse.Checked = false;
            this.chk17_HoldingCells.Checked = false;
            this.chk17_Prision.Checked = false;
            this.chk18_CountyJail.Checked = false;
            this.chk18_Outgunned.Checked = false;
            this.chk18_Burn.Checked = false;
            this.chk18_HopeFair.Checked = false;
            this.chk21_TailorShop.Checked = false;
            this.chk22_BlackwaterPark.Checked = false;
            this.chk22_Penthouse.Checked = false;
            this.chk24_Blackwaterroof.Checked = false;
            this.chk25_CementaryEntrance.Checked = false;
            this.chk25_BurnwoodFamilyTomb.Checked = false;
            this.chk25_Crematorium.Checked = false;
        }

        private void radialPresetKotti_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAutoReset.Checked = true;
            this.chkAutoStart.Checked = true;
            this.chk00Garden.Checked = false;
            this.chk00Greenhouse.Checked = false;
            this.chk00Mansion.Checked = false;
            this.chk00MansionUpper.Checked = true;
            this.chk01ChinatownSquare.Checked = true;
            this.chk02TerminusHotel.Checked = false;
            this.chk02UpperFloors.Checked = true;
            this.chk03BurningHotel.Checked = false;
            this.chk03TheLibrary.Checked = false;
            this.chk03PigeonCoop.Checked = false;
            this.chk03Drugs.Checked = false;
            this.chk03Station.Checked = true;
            this.chk04Courtyard.Checked = false;
            this.chk04Club.Checked = false;
            this.chk04DressingRooms.Checked = false;
            this.chk04DerelictBuilding.Checked = false;
            this.chk04Store.Checked = false;
            this.chk04LoadingArea.Checked = false;
            this.chk04ChineseNewYear.Checked = true;
            this.chk06Victoria.Checked = false;
            this.chk06Orphanage.Checked = false;
            this.chk06CentralHeating.Checked = true;
            this.chk07Balls.Checked = true;
            this.chk08GunShop.Checked = true;
            this.chk09_StretsOfHope.Checked = false;
            this.chk09_Barbershop.Checked = true;
            this.chk10_Desert.Checked = false;
            this.chk11_DeadEnd.Checked = false;
            this.chk11_OldMill.Checked = false;
            this.chk11_Descent.Checked = false;
            this.chk11_FactoryComp.Checked = true;
            this.chk12_TestFacility.Checked = false;
            this.chk12_Decontamination.Checked = false;
            this.chk12_RnD.Checked = true;
            this.chk13_PatriotHangar.Checked = false;
            this.chk13_Arena.Checked = true;
            this.chk14_Parking.Checked = false;
            this.chk14_Reception.Checked = false;
            this.chk14_Cornfield.Checked = true;
            this.chk17_Courthouse.Checked = false;
            this.chk17_HoldingCells.Checked = false;
            this.chk17_Prision.Checked = true;
            this.chk18_CountyJail.Checked = false;
            this.chk18_Outgunned.Checked = false;
            this.chk18_Burn.Checked = false;
            this.chk18_HopeFair.Checked = true;
            this.chk21_TailorShop.Checked = false;
            this.chk22_BlackwaterPark.Checked = false;
            this.chk22_Penthouse.Checked = true;
            this.chk24_Blackwaterroof.Checked = true;
            this.chk25_CementaryEntrance.Checked = false;
            this.chk25_BurnwoodFamilyTomb.Checked = false;
            this.chk25_Crematorium.Checked = true;
        }

        private void radialPresetChaptersOnly_CheckedChanged(object sender, EventArgs e)
        {
            this.chkAutoReset.Checked = true;
            this.chkAutoStart.Checked = true;
            this.chk00Garden.Checked = false;
            this.chk00Greenhouse.Checked = false;
            this.chk00Mansion.Checked = false;
            this.chk00MansionUpper.Checked = true;
            this.chk01ChinatownSquare.Checked = true;
            this.chk02TerminusHotel.Checked = false;
            this.chk02UpperFloors.Checked = true;
            this.chk03BurningHotel.Checked = false;
            this.chk03TheLibrary.Checked = false;
            this.chk03PigeonCoop.Checked = false;
            this.chk03Drugs.Checked = false;
            this.chk03Station.Checked = true;
            this.chk04Courtyard.Checked = false;
            this.chk04Club.Checked = false;
            this.chk04DressingRooms.Checked = false;
            this.chk04DerelictBuilding.Checked = false;
            this.chk04Store.Checked = false;
            this.chk04LoadingArea.Checked = false;
            this.chk04ChineseNewYear.Checked = true;
            this.chk06Victoria.Checked = false;
            this.chk06Orphanage.Checked = false;
            this.chk06CentralHeating.Checked = true;
            this.chk07Balls.Checked = true;
            this.chk08GunShop.Checked = true;
            this.chk09_StretsOfHope.Checked = false;
            this.chk09_Barbershop.Checked = true;
            this.chk10_Desert.Checked = true;
            this.chk11_DeadEnd.Checked = false;
            this.chk11_OldMill.Checked = false;
            this.chk11_Descent.Checked = false;
            this.chk11_FactoryComp.Checked = true;
            this.chk12_TestFacility.Checked = false;
            this.chk12_Decontamination.Checked = false;
            this.chk12_RnD.Checked = true;
            this.chk13_PatriotHangar.Checked = false;
            this.chk13_Arena.Checked = true;
            this.chk14_Parking.Checked = false;
            this.chk14_Reception.Checked = false;
            this.chk14_Cornfield.Checked = true;
            this.chk17_Courthouse.Checked = false;
            this.chk17_HoldingCells.Checked = false;
            this.chk17_Prision.Checked = true;
            this.chk18_CountyJail.Checked = false;
            this.chk18_Outgunned.Checked = false;
            this.chk18_Burn.Checked = false;
            this.chk18_HopeFair.Checked = true;
            this.chk21_TailorShop.Checked = true;
            this.chk22_BlackwaterPark.Checked = false;
            this.chk22_Penthouse.Checked = true;
            this.chk24_Blackwaterroof.Checked = true;
            this.chk25_CementaryEntrance.Checked = false;
            this.chk25_BurnwoodFamilyTomb.Checked = false;
            this.chk25_Crematorium.Checked = true;
        }
    }
}
