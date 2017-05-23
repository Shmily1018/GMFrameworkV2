/*********************************************************
** Copyright (c) 2008 Gold Mantis Co., Ltd. 
** FileName:         BackgroundStyle.cs
** Creator:          Joe
** CreateDate:       2015-08-21
** Changer:
** LastChangeDate:
** Description:      Bootstrap 背景
** VersionInfo:
*********************************************************/

namespace GoldMantis.Web.HtmlControl.Styles
{
    public class BackgroundStyle
    {
        private static BackgroundStyle _instance;

        public readonly string BgBlue = "bg-blue";

        public readonly string BgBlueChambray = "bg-blue-chambray";

        public readonly string BgBlueDark = "bg-blue-dark";

        public readonly string BgBlueEbonyclay = "bg-blue-ebonyclay";

        public readonly string BgBlueHoki = "bg-blue-hoki";

        public readonly string BgBlueMadison = "bg-blue-madison";

        public readonly string BgBlueSharp = "bg-blue-sharp";

        public readonly string BgBlueSoft = "bg-blue-soft";

        public readonly string BgBlueSteel = "bg-blue-steel";

        public readonly string BgDanger = "bg-danger";

        public readonly string BgGreen = "bg-green";

        public readonly string BgGreenHaze = "bg-green-haze";

        public readonly string BgGreenJungle = "bg-green-jungle";

        public readonly string BgGreenMeadow = "bg-green-meadow";

        public readonly string BgGreenSeagreen = "bg-green-seagreen";

        public readonly string BgGreenSharp = "bg-green-sharp";

        public readonly string BgGreenSoft = "bg-green-soft";

        public readonly string BgGreenTurquoise = "bg-green-turquoise";

        public readonly string BgGrey = "bg-grey";

        public readonly string BgGreyCararra = "bg-grey-cararra";

        public readonly string BgGreyCascade = "bg-grey-cascade";

        public readonly string BgGreyGallery = "bg-grey-gallery";

        public readonly string BgGreyMint = "bg-grey-mint";

        public readonly string BgGreySalsa = "bg-grey-salsa";

        public readonly string BgGreySalt = "bg-grey-salt";

        public readonly string BgGreySilver = "bg-grey-silver";

        public readonly string BgGreySteel = "bg-grey-steel";

        public readonly string BgInfo = "bg-info";

        public readonly string BgInverse = "bg-inverse";

        public readonly string BgPrimary = "bg-primary";

        public readonly string BgPurple = "bg-purple";

        public readonly string BgPurpleIntense = "bg-purple-intense";

        public readonly string BgPurpleMedium = "bg-purple-medium";

        public readonly string BgPurplePlum = "bg-purple-plum";

        public readonly string BgPurpleSeance = "bg-purple-seance";

        public readonly string BgPurpleSharp = "bg-purple-sharp";

        public readonly string BgPurpleSoft = "bg-purple-soft";

        public readonly string BgPurpleStudio = "bg-purple-studio";

        public readonly string BgPurpleWisteria = "bg-purple-wisteria";

        public readonly string BgRed = "bg-red";

        public readonly string BgRedFlamingo = "bg-red-flamingo";

        public readonly string BgRedHaze = "bg-red-haze";

        public readonly string BgRedIntense = "bg-red-intense";

        public readonly string BgRedPink = "bg-red-pink";

        public readonly string BgRedSoft = "bg-red-soft";

        public readonly string BgRedSunglo = "bg-red-sunglo";

        public readonly string BgRedThunderbird = "bg-red-thunderbird";

        public readonly string BgSuccess = "bg-success";

        public readonly string BgWarning = "bg-warning";

        public readonly string BgYellow = "bg-yellow";

        public readonly string BgYellowCasablanca = "bg-yellow-casablanca";

        public readonly string BgYellowCrusta = "bg-yellow-crusta";

        public readonly string BgYellowGold = "bg-yellow-gold";

        public readonly string BgYellowLemon = "bg-yellow-lemon";

        public readonly string BgYellowSaffron = "bg-yellow-saffron";

        static BackgroundStyle()
        {
            _instance = new BackgroundStyle();
        }

        private BackgroundStyle()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static BackgroundStyle Instance
        {
            get { return _instance; }
        }
    }
}