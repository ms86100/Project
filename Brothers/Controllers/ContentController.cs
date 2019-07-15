using Brothers.Entities.DataAccess;
using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Brothers.Entities.ViewModels;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;

namespace Brothers.Controllers
{
    public class ContentController : Controller
    {
        #region Who we are
        public ActionResult Profile()
        {
            return View();
        }
        public ActionResult TeamMessage()
        {
            return View();
        }
        public ActionResult BrothersFleetService()
        {
            return View();
        }
        public ActionResult BrothersPhilosophy()
        {
            return View();
        }
        public ActionResult WhyBrothersTeam()
        {
            return View();
        }
        public ActionResult BrothersAffiliation()
        {
            return View();
        }
        public ActionResult ServicesProvided()
        {
            return View();
        }
        public ActionResult PartnerWithUs()
        {
            return View();
        }

        #endregion

        #region customer care
        public ActionResult FAQs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult EmergencyCoordinates()
        {
            return View();
        }
        public ActionResult Remittance()
        {
            return View();
        }
        #endregion customer care

        #region Sikkim
        public ActionResult AboutSikkim()
        {
            return View();
        }
        public ActionResult SikkimPeople()
        {
            return View();
        }
        public ActionResult SikkimFestivals()
        {
            return View();
        }
        public ActionResult SikkimClimate()
        {
            return View();
        }
        public ActionResult SikkimHistory()
        {
            return View();
        }
        public ActionResult SikkimGeography()
        {
            return View();
        }
        public ActionResult SikkimEconomy()
        {
            return View();
        }
        public ActionResult NorthSikkim()
        {
            return View();
        }
        public ActionResult WestSikkim()
        {
            return View();
        }
        public ActionResult SouthSikkim()
        {
            return View();
        }
        public ActionResult Mountaineering()
        {
            return View();
        }
        public ActionResult WhyTrekInSikkim()
        {
            return View();
        }
        public ActionResult WhenToTrek()
        {
            return View();
        }
        public ActionResult TrekkingPermits()
        {
            return View();
        }
        public ActionResult MountaineeringTrekking()
        {
            return View();
        }
        public ActionResult MedicalMatters()
        {
            return View();
        }
        public ActionResult PeaksOfSikkim()
        {
            return View();
        }
        public ActionResult TrekkingPeaks()
        {
            return View();
        }
        public ActionResult Rafting()
        {
            return View();
        }
        public ActionResult Paragliding()
        {
            return View();
        }
        public ActionResult MountainFlights()
        {
            return View();
        }
        public ActionResult Ropeway()
        {
            return View();
        }
        public ActionResult MountainBiking()
        {
            return View();
        }
        public ActionResult Boating()
        {
            return View();
        }
        public ActionResult Fishing()
        {
            return View();
        }
        public ActionResult RockClimbing()
        {
            return View();
        }
        public ActionResult YakSafari()
        {
            return View();
        }
        public ActionResult Archery()
        {
            return View();
        }
        public ActionResult Casino()
        {
            return View();
        }
        public ActionResult ShoppingGuide()
        {
            return View();
        }
        public ActionResult Nightlife()
        {
            return View();
        }
        public ActionResult Restaurants()
        {
            return View();
        }
        public ActionResult SikkimReligion()
        {
            return View();
        }
        public ActionResult SikkimGovernment()
        {
            return View();
        }
        public ActionResult FactsAndFigures()
        {
            return View();
        }
        public ActionResult DosAndDonts()
        {
            return View();
        }
        public ActionResult ArtsAndArchitecture()
        {
            return View();
        }
        public ActionResult NationalParks()
        {
            return View();
        }
        public ActionResult HeritageSites()
        {
            return View();
        }
        public ActionResult EnteringLeaving()
        {
            return View();
        }
        public ActionResult PriorToEntry()
        {
            return View();
        }
        public ActionResult AfterArrival()
        {
            return View();
        }
        public ActionResult TrekkingPermit()
        {
            return View();
        }
        public ActionResult ExpeditionPermit()
        {
            return View();
        }
        public ActionResult InnerLinePermit()
        {
            return View();
        }
        public ActionResult ForIndianNationals()
        {
            return View();
        }
        public ActionResult InsideGangtok()
        {
            return View();
        }
        public ActionResult AroundGangtok()
        {
            return View();
        }
        public ActionResult OutsideGangtok()
        {
            return View();
        }
        #endregion Sikkim

        #region West Bengal
        public ActionResult WestBengal()
        {
            return View();
        }
        public ActionResult wbMountaineering()
        {
            return View();
        }
        public ActionResult whytrek()
        {
            return View();
        }
        public ActionResult wbWhenToTrek()
        {
            return View();
        }
        public ActionResult wbTrekkingPermit()
        {
            return View();
        }
        public ActionResult wbMedicalMatters()
        {
            return View();
        }
        public ActionResult wbRafting()
        {
            return View();
        }
        public ActionResult wbParagliding()
        {
            return View();
        }
        public ActionResult wbHotAirBalloon()
        {
            return View();
        }
        public ActionResult wbRopeway()
        {
            return View();
        }
        public ActionResult wbMountainBiking()
        {
            return View();
        }
        public ActionResult wbBoating()
        {
            return View();
        }
        public ActionResult wbFishing()
        {
            return View();
        }
        public ActionResult wbRockClimbing()
        {
            return View();
        }
        public ActionResult wbYachting()
        {
            return View();
        }
        public ActionResult wbCarRally()
        {
            return View();
        }
        public ActionResult wbWaterSkiing()
        {
            return View();
        }
        public ActionResult wbJeepSafari()
        {
            return View();
        }
        public ActionResult wbElephantRide()
        {
            return View();
        }
        public ActionResult wbTeaPlantation()
        {
            return View();
        }
        public ActionResult wbCruises()
        {
            return View();
        }
        public ActionResult wbWalks()
        {
            return View();
        }
        public ActionResult wbTypicalActivities()
        {
            return View();
        }
        public ActionResult wbCuisine()
        {
            return View();
        }
        public ActionResult wbShoppingGuide()
        {
            return View();
        }
        public ActionResult wbTollywood()
        {
            return View();
        }
        public ActionResult wbNightLife()
        {
            return View();
        }
        public ActionResult wbGeography()
        {
            return View();
        }
        public ActionResult wbHistory()
        {
            return View();
        }
        public ActionResult wbReligion()
        {
            return View();
        }
        public ActionResult wbEconomy()
        {
            return View();
        }
        public ActionResult wbGovernment()
        {
            return View();
        }
        public ActionResult wbPeople()
        {
            return View();
        }
        public ActionResult wbFestivals()
        {
            return View();
        }
        public ActionResult wbFactsAndFigures()
        {
            return View();
        }
        public ActionResult wbDoDonts()
        {
            return View();
        }
        public ActionResult wbClimate()
        {
            return View();
        }
        public ActionResult wbArtandArchitecture()
        {
            return View();
        }
        public ActionResult wbNationalParks()
        {
            return View();
        }
        public ActionResult wbHeritageSites()
        {
            return View();
        }
        public ActionResult wbEnteringLeaving()
        {
            return View();
        }
        public ActionResult wbGeneralRequirement()
        {
            return View();
        }
        public ActionResult wbCurrency()
        {
            return View();
        }
        public ActionResult wbInsideKolkata()
        {
            return View();
        }
        public ActionResult wbAroundKolkata()
        {
            return View();
        }
        #endregion West Bengal

        #region Darjeeling
        public ActionResult AboutDarjeeling()
        {
            return View();
        }
        public ActionResult DarjeelingPeople()
        {
            return View();
        }
        public ActionResult DarjFloraandFauna()
        {
            return View();
        }
        public ActionResult DarjeelingHistory()
        {
            return View();
        }
        public ActionResult DarjeelingEconomy()
        {
            return View();
        }
        public ActionResult DarjeelingPlaces()
        {
            return View();
        }
        public ActionResult AroundDarjeeling()
        {
            return View();
        }
        public ActionResult OutsideDarjeeling()
        {
            return View();
        }
        public ActionResult DarjeelingFestivals()
        {
            return View();
        }
        #endregion Darjeeling

        #region Dooars
        public ActionResult AboutDooars()
        {
            return View();
        }
        public ActionResult DooarsEnteringLeaving()
        {
            return View();
        }
        public ActionResult EasternDooars()
        {
            return View();
        }
        public ActionResult CentralDooars()
        {
            return View();
        }
        public ActionResult WesternDooars()
        {
            return View();
        }
        #endregion Dooars

        #region Bhutan
        public ActionResult BhutanTrekking()
        {
            return View();
        }
        public ActionResult BhutanWhyTrek()
        {
            return View();
        }
        public ActionResult BhutanWhenToTrek()
        {
            return View();
        }
        public ActionResult BhutanTrekkingPermit()
        {
            return View();
        }
        public ActionResult BhutanMedicalMatters()
        {
            return View();
        }
        public ActionResult BhutanRafting()
        {
            return View();
        }
        public ActionResult BhutanBiking()
        {
            return View();
        }
        public ActionResult BhutanFishing()
        {
            return View();
        }
        public ActionResult BhutanArchery()
        {
            return View();
        }
        public ActionResult BhutanFloraandFauna()
        {
            return View();
        }
        public ActionResult BhutanNationalParks()
        {
            return View();
        }
        public ActionResult BhutanShopping()
        {
            return View();
        }
        public ActionResult BhutanGeography()
        {
            return View();
        }
        public ActionResult BhutanHistory()
        {
            return View();
        }
        public ActionResult BhutanReligion()
        {
            return View();
        }
        public ActionResult BhutanGovernment()
        {
            return View();
        }
        public ActionResult BhutanNature()
        {
            return View();
        }
        public ActionResult BhutanPeople()
        {
            return View();
        }
        public ActionResult BhutanFestival()
        {
            return View();
        }
        public ActionResult BhutanSeason()
        {
            return View();
        }
        public ActionResult BhutanDosAndDonts()
        {
            return View();
        }
        public ActionResult BhutanFood()
        {
            return View();
        }
        public ActionResult BhutanDistanceChart()
        {
            return View();
        }
        public ActionResult BhutanAverageTemperature()
        {
            return View();
        }
        public ActionResult BhutanEnteringLeaving()
        {
            return View();
        }
        public ActionResult BhutanForeignNationals()
        {
            return View();
        }
        public ActionResult BhutanIndianNationals()
        {
            return View();
        }
        public ActionResult BhutanFAQ()
        {
            return View();
        }
        public ActionResult BhutanInsideParo()
        {
            return View();
        }
        public ActionResult BhutanThimphu()
        {
            return View();
        }
        public ActionResult BhutanPhuentsholing()
        {
            return View();
        }
        public ActionResult BhutanPunakha()
        {
            return View();
        }
        public ActionResult BhutanWangduephodrang()
        {
            return View();
        }
        public ActionResult BhutanGangtey()
        {
            return View();
        }
        public ActionResult BhutanTrongsa()
        {
            return View();
        }
        public ActionResult BhutanBumthang()
        {
            return View();
        }
        public ActionResult BhutanMongar()
        {
            return View();
        }
        public ActionResult BhutanTrashigang()
        {
            return View();
        }
        public ActionResult BhutanTrashiyangtse()
        {
            return View();
        }
        public ActionResult BhutanSamdrup()
        {
            return View();
        }
        public ActionResult AboutBhutan()
        {
            return View();
        }
        #endregion Bhutan

        #region Goa
        public ActionResult AboutGoa()
        {
            return View();
        }
        public ActionResult GoaTrek()
        {
            return View();
        }
        public ActionResult GoaWhytrek()
        {
            return View();
        }
        public ActionResult GoaPopulartrek()
        {
            return View();
        }
        public ActionResult GoaScuba()
        {
            return View();

        }
        public ActionResult Goasailing()
        {
            return View();
        }
        public ActionResult Goasports()
        {
            return View();
        }
        public ActionResult Goahiking()
        {
            return View();
        }
        public ActionResult Goawildlife()
        {
            return View();
        }
        public ActionResult Goacasino()
        {
            return View();
        }
        public ActionResult Goashopping()
        {
            return View();
        }
        public ActionResult Goanightlife()
        {
            return View();
        }
        public ActionResult GoaRestaurant()
        {
            return View();
        }
        public ActionResult GoaGeography()
        {
            return View();
        }
        public ActionResult GoaHistory()
        {
            return View();
        }
        public ActionResult GoaReligion()
        {
            return View();
        }
        public ActionResult GoaEconomy()
        {
            return View();
        }
        public ActionResult GoaCultutre()
        {
            return View();
        }
        public ActionResult GoaPeople()
        {
            return View();
        }
        public ActionResult GoaFestival()
        {
            return View();
        }
        public ActionResult GoaFacts()
        {
            return View();
        }
        public ActionResult GoaClimate()
        {
            return View();
        }
        public ActionResult GoaEntering()
        {
            return View();
        }
        public ActionResult GoaRegistration()
        {
            return View();
        }
        public ActionResult GoaBeaches()
        {
            return View();
        }
        public ActionResult Goachurches()
        {
            return View();
        }
        public ActionResult GoaCuisine()
        {
            return View();
        }
        public ActionResult GoaTourist()
        {
            return View();
        }




        #endregion Goa

        #region jammu and kashmir
        public ActionResult AboutJammu()
        {
            return View();
        }
        public ActionResult JammuTrekking()
        {
            return View();
        }

         public ActionResult JammuWhentrek()
        {
            return View();
        }
         public ActionResult JammuTips()
         {
             return View();
         }
         public ActionResult JammuTrek()
         {
             return View();

         }

         public ActionResult JammuSkiing()
         {
             return View();
         }
         public ActionResult JammuAero()
         {
             return View();
         }
         public ActionResult JammuWater()
         {
             return View();
         }
         public ActionResult jammuGolf()
         {
             return View();
         }
         public ActionResult JammuAngling()
         {
             return View();
         }
         public ActionResult JammuGeography()
         {
             return View();
         }

         public ActionResult JammuHistory()
         {
             return View();
         }
         public ActionResult JammuReligion()
         {
             return View();
         }
         public ActionResult JammuLanguage()
         {
             return View();
         }
         public ActionResult JammuEconomy()
         {
             return View();
         }
         public ActionResult Jammupeople()
         {
             return View();
         }
         public ActionResult Jammucustom()
         {
             return View();
         }
         public ActionResult JammuFacts()
         {
             return View();
         }
         public ActionResult JammuEntering()
         {
             return View();
         }
         public ActionResult JammuTosee()
         {
             return View();
         }


        #endregion jammu and kashmir

        #region Nepal
        public ActionResult AboutNepal()
        {
            return View();
        }
        public ActionResult npMountaineering()
        {
            return View();
        }
        public ActionResult npWhyTrek()
        {
            return View();
        }
        public ActionResult npWhenToTrek()
        {
            return View();
        }
        public ActionResult npTrekkingPermit()
        {
            return View();
        }
        public ActionResult npMedicalMatters()
        {
            return View();
        }
        public ActionResult npHimalaya()
        {
            return View();
        }
        public ActionResult npTopPeaks()
        {
            return View();
        }
        public ActionResult npTrekkingPeaks()
        {
            return View();
        }
        public ActionResult npRafting()
        {
            return View();
        }
        public ActionResult npParagliding()
        {
            return View();
        }
        public ActionResult npMountainFlight()
        {
            return View();
        }
        public ActionResult MountaineeringInNepal()
        {
            return View();
        }
        public ActionResult npRopeway()
        {
            return View();
        }
        public ActionResult npMountainBiking()
        {
            return View();
        }
        public ActionResult npBoating()
        {
            return View();
        }
        public ActionResult npFishing()
        {
            return View();
        }
        public ActionResult npRockClimbing()
        {
            return View();
        }
        public ActionResult npJungleSafari()
        {
            return View();
        }
        public ActionResult npGolf()
        {
            return View();
        }
        public ActionResult npCasino()
        {
            return View();
        }
        public ActionResult npMuseums()
        {
            return View();
        }
        public ActionResult npShoppingGuide()
        {
            return View();
        }
        public ActionResult npMeditation()
        {
            return View();
        }
        public ActionResult npRestaurant()
        {
            return View();
        }

        public ActionResult NepalGeography()
        {
            return View();
        }
        public ActionResult NepalHistory()
        {
            return View();
        }
        public ActionResult NepalReligion()
        {
            return View();
        }
        public ActionResult NepalEconomy()
        {
            return View();
        }
        public ActionResult NepalGovernment()
        {
            return View();
        }
        public ActionResult NepalPeople()
        {
            return View();
        }
        public ActionResult NepalFestivals()
        {
            return View();
        }
        public ActionResult NepalFactsFigures()
        {
            return View();
        }
        public ActionResult NepalDosAndDonts()
        {
            return View();
        }
        public ActionResult NepalClimate()
        {
            return View();
        }
        public ActionResult NepalArtAndArchitecture()
        {
            return View();
        }
        public ActionResult NepalNationalParks()
        {
            return View();
        }
        public ActionResult NepalHeritageSites()
        {
            return View();
        }
        public ActionResult npEnteringLeaving()
        {
            return View();
        }
        public ActionResult npVisa()
        {
            return View();
        }
        public ActionResult npAirportTax()
        {
            return View();
        }
        public ActionResult NepalTrekkingPermit()
        {
            return View();
        }
        public ActionResult InsideKathmandu()
        {
            return View();
        }
        public ActionResult AroundKathmandu()
        {
            return View();
        }
        public ActionResult OutsideKathmandu()
        {
            return View();
        }
        #endregion Nepal

        #region North East
        #region Assam
        public ActionResult AboutAssam()
        {
            return View();
        }

        public ActionResult AssamTrekking()
        {
            return View();
        }

        public ActionResult AssamWhyTrek()
        {
            return View();
        }

        public ActionResult AssamWhenToTrek()
        {
            return View();
        }

        public ActionResult AssamPermit()
        {
            return View();
        }
        public ActionResult AssamMedicalAdvice()
        {
            return View();
        }
        public ActionResult AssamHimalayas()
        {
            return View();
        }

        public ActionResult AssamRafting()
        {
            return View();
        }
        public ActionResult AssamaAngling()
        {
            return View();
        }
        public ActionResult AssamaRiverCruise()
        {
            return View();
        }

        public ActionResult AssamaBoatRacing()
        {
            return View();
        }
        public ActionResult AssamaGolf()
        {
            return View();
        }

        public ActionResult AssamaMountainBiking()
        {
            return View();
        }

        public ActionResult AssamaParaSailing()
        {
            return View();
        }

        public ActionResult AssamaHangGliding()
        {
            return View();
        }

        public ActionResult AssamaArchery()
        {
            return View();
        }

        public ActionResult AssamaCamping()
        {
            return View();
        }

        public ActionResult AssamaBiking()
        {
            return View();
        }

        public ActionResult AssamaRockClimbing()
        {
            return View();
        }

        public ActionResult AssamMuseum()
        {
            return View();
        }

        public ActionResult AssamShoppingGuide()
        {
            return View();
        }

        public ActionResult AssamRestaurant()
        {
            return View();
        }

        public ActionResult AssamNightClubs()
        {
            return View();
        }

        public ActionResult AssamTea()
        {
            return View();
        }

        public ActionResult AssamArtCraft()
        {
            return View();
        }

        public ActionResult AssamWildife()
        {
            return View();
        }
        public ActionResult AssamJewelleries()
        {
            return View();
        }

        public ActionResult AssamGeography()
        {
            return View();
        }
        public ActionResult AssamHistory()
        {
            return View();
        }
        public ActionResult AssamReligion()
        {
            return View();
        }
        public ActionResult AssamEconomy()
        {
            return View();
        }
        public ActionResult AssamGovernment()
        {
            return View();
        }
        public ActionResult AssamAgriculture()
        {
            return View();
        }
        public ActionResult AssamPeopleOfAssam()
        {
            return View();
        }
        public ActionResult AssamFestivals()
        {
            return View();
        }
        public ActionResult AssamFactsAndFigures()
        {
            return View();
        }
        public ActionResult AssamDoandDnt()
        {
            return View();
        }
        public ActionResult AssamClimates()
        {
            return View();
        }
        public ActionResult AssamNationaklParks()
        {
            return View();
        }
        public ActionResult AssamReligiousPlace()
        {
            return View();
        }
        public ActionResult AssamEnteringLeaving()
        {
            return View();
        }
        public ActionResult AssamInsideGuwahati()
        {
            return View();
        }
        public ActionResult AssamAroundGuwahati()
        {
            return View();
        }
        public ActionResult AssamOutsideGuwahati()
        {
            return View();
        }



        #endregion Assam
        #region Arunachal-Pradesh
        public ActionResult AboutArunachalPradesh()
        {
            return View();
        }

        public ActionResult APTrek()
        {
            return View();
        }
        public ActionResult APWhyTrek()
        {
            return View();
        }
        public ActionResult APWhenTrek()
        {
            return View();
        }
        public ActionResult APTrekPermit()
        {
            return View();
        }
        public ActionResult APMedical()
        {
            return View();
        }
        public ActionResult APHimalaya()
        {
            return View();
        }

        public ActionResult APRafting()
        {
            return View();
        }

        public ActionResult APAngling()
        {
            return View();
        }

        public ActionResult APBoating()
        {
            return View();
        }

        public ActionResult APWhiteWaterRafting()
        {
            return View();
        }
        public ActionResult APMountainBiking()
        {
            return View();
        }

        public ActionResult APMountaineering()
        {
            return View();
        }

        public ActionResult APRockClimbing()
        {
            return View();
        }
        public ActionResult ApScuba()
        {
            return View();
        }

        public ActionResult ApMuseum()
        {
            return View();
        }

        public ActionResult ApShopping()
        {
            return View();
        }
        public ActionResult ApRestaurant()
        {
            return View();
        }
        public ActionResult ApArt()
        {
            return View();
        }
        public ActionResult ApWildlife()
        {
            return View();
        }

        public ActionResult ApJewelleries()
        {
            return View();
        }

        public ActionResult Apgeography()
        {
            return View();
        }
        public ActionResult ApHistory()
        {
            return View();
        }
        public ActionResult Apreligion()
        {
            return View();
        }
        public ActionResult ApEconomy()
        {
            return View();
        }
        public ActionResult ApGovernment()
        {
            return View();
        }
        public ActionResult ApCulture()
        {
            return View();
        }
        public ActionResult ApPeople()
        {
            return View();
        }
        public ActionResult ApFestival()
        {
            return View();
        }
        public ActionResult ApFacts()
        {
            return View();
        }
        public ActionResult ApClimates()
        {
            return View();
        }
        public ActionResult ApEntering()
        {
            return View();
        }
        public ActionResult ApInside()
        {
            return View();
        }

        public ActionResult Apoutside()
        {
            return View();
        }



        #endregion Arunachal-Pradesh
        #region Manipur

        public ActionResult AboutManipur()
        {
            return View();
        }
        public ActionResult ManipurTrek()
        {
            return View();
        }

        public ActionResult ManipurWhyTrek()
        {
            return View();
        }
        public ActionResult ManipurWhenTrek()
        {
            return View();
        }
        public ActionResult ManipurMedical()
        {
            return View();
        }
        public ActionResult Manipurmajortrek()
        {
            return View();
        }
        #endregion Manipur
        #region Meghalaya

        public ActionResult AboutMeghalaya()
        {
            return View();
        }
        public ActionResult MeghalayaTrek()
        {
            return View();
        }
        public ActionResult MeghalayaWhyTrek()
        {
            return View();
        }
        public ActionResult MeghalayawhenTrek()
        {
            return View();
        }
        public ActionResult MeghalayaMedical()
        {
            return View();
        }
        public ActionResult MeghalayaMajortrek()
        {
            return View();
        }

        public ActionResult MeghalayaAngling()
        {
            return View();
        }

        public ActionResult MeghalayaTrekking()
        {
            return View();
        }

        public ActionResult MeghalayaCaving()
        {
            return View();
        }

        public ActionResult MeghalayaGolfing()
        {
            return View();
        }

        public ActionResult MeghalayaArchery()
        {
            return View();
        }

        public ActionResult MeghalayaBoating()
        {
            return View();
        }

        public ActionResult MeghalayaCamping()
        {
            return View();
        }

        public ActionResult MeghalayaMuseum()
        {
            return View();
        }

        public ActionResult MeghalayaShopping()
        {
            return View();
        }

        public ActionResult MeghalayaRestaurant()
        {
            return View();
        }

        public ActionResult MeghalayaNightLife()
        {
            return View();
        }

        public ActionResult MeghalayaArt()
        {
            return View();
        }

        public ActionResult MeghalayaTextile()
        {
            return View();
        }

        public ActionResult MeghalayaJewellery()
        {
            return View();
        }
        public ActionResult MeghalayaGeography()
        {
            return View();
        }
        public ActionResult MeghalayaHistory()
        {
            return View();
        }
        public ActionResult MeghalayaReligion()
        {
            return View();
        }
        public ActionResult MeghalayaEconomy()
        {
            return View();
        }
        public ActionResult MeghalayaAgriculture()
        {
            return View();
        }
        public ActionResult MeghalayaPeople()
        {
            return View();
        }
        public ActionResult Meghalayafestival()
        {
            return View();
        }
        public ActionResult MeghalayaFacts()
        {
            return View();
        }
        public ActionResult Meghalayathingstodo()
        {
            return View();
        }
        public ActionResult MeghalayaClimate()
        {
            return View();
        }
        public ActionResult MeghalayaEntering()
        {
            return View();
        }
        public ActionResult MeghalayaEntry()
        {
            return View();
        }
        public ActionResult MeghalayaTips()
        {
            return View();
        }
        public ActionResult MeghalayaInside()
        {
            return View();
        }
        public ActionResult MeghalayaAroud()
        {
            return View();
        }
        public ActionResult MeghalayaOutside()
        {
            return View();
        }



        #endregion Meghalaya
        #region Mizoram
        public ActionResult AboutMizoram()
        {
            return View();
        }
        public ActionResult MizoramTrek()
        {
            return View();
        }
        public ActionResult MizoramWhyTrek()
        {
            return View();
        }
        public ActionResult MizoramWhenTrek()
        {
            return View();
        }
        public ActionResult MizoramMedical()
        {
            return View();
        }
        public ActionResult MizoramMajorTrek()
        {
            return View();
        }
        public ActionResult MizoramAngling()
        {
            return View();
        }

        public ActionResult MizoramBoating()
        {
            return View();
        }
        public ActionResult MizoramCamping()
        {
            return View();
        }
        public ActionResult MizoramCaving()
        {
            return View();
        }
        public ActionResult MizoramMountaineering()
        {
            return View();
        }
        public ActionResult MizoramTrekking()
        {
            return View();
        }

        public ActionResult MizoramShopping()
        {
            return View();
        }
        public ActionResult MizoramArts()
        {
            return View();
        }
        public ActionResult MizoramRestaurants()
        {
            return View();
        }
        public ActionResult MizoramJewelleries()
        {
            return View();
        }

        public ActionResult MizoramGeography()
        {
            return View();
        }

        public ActionResult MizoramHistory()
        {
            return View();
        }
        public ActionResult MizoramReligion()
        {
            return View();
        }

        public ActionResult MizoramEconomy()
        {
            return View();
        }

        public ActionResult MizoramGovernment()
        {
            return View();
        }

        public ActionResult MizoramAgriculture()
        {
            return View();
        }

        public ActionResult MizoramPeople()
        {
            return View();
        }

        public ActionResult MizoramFestival()
        {
            return View();
        }
        public ActionResult MizoramFacts()
        {
            return View();
        }
        public ActionResult MizoramThingstodo()
        {
            return View();
        }
        public ActionResult Mizoramclimate()
        {
            return View();
        }
        public ActionResult MizoramEntering()
        {
            return View();
        }
        public ActionResult MizoramPermit()
        {
            return View();
        }
        public ActionResult MizoramTips()
        {
            return View();
        }
        public ActionResult MizoramInside()
        {
            return View();
        }
        public ActionResult MizoramAround()
        {
            return View();
        }
        public ActionResult MizoramOutside()
        {
            return View();
        }







        #endregion Mizoram
        #region nagaland
        public ActionResult AboutNagaland()
        {
            return View();
        }
        public ActionResult NagalandTrek()
        {
            return View();
        }
        public ActionResult NagalandWhyTrek()
        {
            return View();
        }
        public ActionResult NagalandWhenTrek()
        {
            return View();
        }
        public ActionResult Nagalandpermit()
        {
            return View();
        }
        public ActionResult NagalandMedical()
        {
            return View();
        }
        public ActionResult NagalandMajorTrek()
        {
            return View();
        }
        public ActionResult NagalandAngling()
        {
            return View();
        }
        public ActionResult NagalandCamping()
        {
            return View();
        }
        public ActionResult NagalandTrekking()
        {
            return View();
        }
        public ActionResult NagalandMountainBiking()
        {
            return View();
        }
        public ActionResult NagalandMuseum()
        {
            return View();
        }
        public ActionResult NagalandShopping()
        {
            return View();
        }
        public ActionResult NagalandRestaurant()
        {
            return View();
        }
        public ActionResult NagalandArt()
        {
            return View();
        }
        public ActionResult NagalandWildlife()
        {
            return View();
        }
        public ActionResult NagalandTextile()
        {
            return View();
        }

        public ActionResult NagalandGeography()
        {
            return View();
        }

        public ActionResult NagalandHistory()
        {
            return View();
        }

        public ActionResult Nagalandreligion()
        {
            return View();
        }

        public ActionResult NagalandEconomy()
        {
            return View();
        }

        public ActionResult NagalandAgriculture()
        {
            return View();
        }

        public ActionResult NagalandPeople()
        {
            return View();
        }

        public ActionResult Nagalandfestival()
        {
            return View();
        }

        public ActionResult NagalandFacts()
        {
            return View();
        }

        public ActionResult NagalandDosndonats()
        {
            return View();
        }

        public ActionResult NagalandClimate()
        {
            return View();
        }

        public ActionResult NagalandReligiousplace()
        {
            return View();
        }
        public ActionResult NagalandEntering()
        {
            return View();
        }
        public ActionResult NagaPermit()
        {
            return View();
        }
        public ActionResult NagalandTodo()
        {
            return View();
        }
        public ActionResult NagalandAdvice()
        {
            return View();
        }


        public ActionResult NagalandInside()
        {
            return View();
        }
        public ActionResult NagalandAround()
        {
            return View();
        }
        public ActionResult NagalandOutside()
        {
            return View();
        }



        #endregion nagaland




        #endregion North East

        #region GoldenTriangle
        public ActionResult GoldenTriangleTour()
        {
            return View();
        }
        public ActionResult AboutDelhi()
        {
            return View();
        }
        public ActionResult DelhiMonuments()
        {
            return View();
        }
        public ActionResult DelhiTemples()
        {
            return View();
        }
        public ActionResult DelhiGardens()
        {
            return View();
        }
        public ActionResult DelhiExcursions()
        {
            return View();
        }
        public ActionResult DelhiFair()
        {
            return View();
        }
        public ActionResult DelhiShopping()
        {
            return View();
        }
        public ActionResult AboutAgra()
        {
            return View();
        }
        public ActionResult AgraMonuments()
        {
            return View();
        }
        public ActionResult AgraTemples()
        {
            return View();
        }
        public ActionResult AgraGardens()
        {
            return View();
        }
        public ActionResult AgraExcursions()
        {
            return View();
        }
        public ActionResult AgraFairs()
        {
            return View();
        }
        public ActionResult AgraShopping()
        {
            return View();
        }
        public ActionResult AboutJaipur()
        {
            return View();
        }
        public ActionResult JaipurMonuments()
        {
            return View();
        }
        public ActionResult JaipurTemples()
        {
            return View();
        }
        public ActionResult JaipurGardens()
        {
            return View();
        }
        public ActionResult JaipurExcursions()
        {
            return View();
        }
        public ActionResult JaipurFairs()
        {
            return View();
        }
        public ActionResult JaipurShopping()
        {
            return View();
        }
        #endregion

        dalGenEnquiry obj = new dalGenEnquiry();

        public ActionResult Create()
        {
            return PartialView("_pvEnquiry");
        }
        [HttpPost]
        public ActionResult Create(utblGenEnquiry model)
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
            //Create the SMTP Client
            SmtpClient client = new SmtpClient();
            client.Host = settings.Smtp.Network.Host;
            client.Credentials = credential;
            client.Timeout = 300000;
            client.EnableSsl = false;
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                StringBuilder mailbody = new StringBuilder();
                mail.From = new MailAddress(settings.Smtp.Network.UserName, "Brothers Tours & Travel");
                mail.To.Add(model.GuestEmailID);
                mail.To.Add("brothersreservation@gmail.com");
                mail.Priority = MailPriority.High;
                mail.ReplyToList.Add("brothersreservation@gmail.com");
                mail.Subject = "Enquiry Details";
                mailbody.Append("<b>Thank You for submitting your enquiry. Our Travel Agents with get in touch with you soon with answers to your enquiry.</b>");
                mailbody.Append("<h4>Enquiry Details</h4>");
                mailbody.Append("Email Address: " + model.GuestEmailID + "<br/>Contact No.: " + model.GuestMobileNo + "<br/>Enquiry : " + model.EnquiryDetails);
                mail.Body = mailbody.ToString();
                mail.IsBodyHtml = true;
                DateTime Currdate = DateTime.Today;
                string tempdate = Currdate.ToString("dd MMM yyyy");
                DateTime NewCurrDate = DateTime.ParseExact(tempdate, "dd MMM yyyy", CultureInfo.InvariantCulture);
                model.EnquiryDate = NewCurrDate;
                model.EnquiryStatus = "Not Replied";
                try
                {
                    client.Send(mail);
                    var a = obj.Save(model);
                    return Json(new { success = true, Message = 1 });
                }
                catch (Exception)
                {
                    return Json(new { success = true, Message = 0 });
                }
            }
            return PartialView("_pvEnquiry", model);
        }
    }

}