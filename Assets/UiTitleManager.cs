﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UniRx;

public enum TitleMissionId
{
    Level1000,//★
    Level2000,//★
    Level3000,//★
    Level5000,//★
    Level7000,//★
    Level9000,//★
    Level11000,//★
    Level13000,//★
    Level15000,//★
    Level17000,//★
    Level20000,//★
    Stage100,//★
    Stage150,//★
    Stage200,//★
    Stage250,//★
    Stage300,//★
    Stage350,//★
    Stage400,//★
    GetLegendWeapon,//★
    GetYomulWeapon,//★
    GetLegendNorigae,//★
    GetSinMulNorigae,//★
    AwakeMarble,//★
    EvolutionPet,//★
    Yomul0,//★
    Yomul1,//★
    Yomul2,//★
    Yomul3,//★

    Stage450,//★
    Stage500,//★
    Stage550,//★
    Stage600,//★
    Stage650,//★

    Level23000,//★
    Level26000,//★
    Level29000,//★
    Stage700,//★
    Stage750,//★
    Stage800,//★
    Stage850,//★
    Level32000,//★
    Level35000,//★
    Level38000,//★
    Yomul4,//★
    Yomul5,//★
    Stage900,//★
    Stage950,//★
    HyeonMu_1,//★
    BaekHo_1,//★
    ZuZak_1,//★

    Level41000,//★
    Level44000,//★
    Level47000,//★
    Level50000,//★
    Stage1000,//★
    Stage1050,//★
    Yomul6,
    Stage1100,//★
    Stage1150,//★
    ChungRyong_1,//★
    Level53000,//★
    Level56000,//★
    Level59000,//★
    Level62000,//★
    Level65000,//★
    Level68000,//★
    Level71000,//★
    Level74000,//★
    Level77000,//★
    Level80000,//★
    Level83000,//★
    Stage1200,//★
    Stage1250,//★
    Stage1300,//★
    Stage1350,//★
    Stage1400,//★
    Yomul7,
    Stage1450,//★
    Stage1500,//★

    Stage1550,//★
    Stage1600,//★

    Stage1650,//★
    Stage1700,//★

    Stage1750,//★
    Stage1800,//★
    GetYachaWeaopon,
    Stage1850,//★
    Stage1900,//★
    Stage1950,//★
    Stage2000,//★
    Stage2050,//★
    Stage2100,//★
    Stage2150,//★
    Stage2200,//★
    Level86000,//★
    Level89000,//★
    Level92000,//★
    Level95000,//★
    Level98000,//★
    Level101000,//★

    Level104000,//★
    Level107000,//★
    Level110000,//★
    Level113000,//★
    Level116000,//★
    Level119000,//★
    Level122000,//★
    Level125000,//★
    Stage2250,//★
    Stage2300,//★
    Stage2350,//★
    Stage2400,//★

    Stage2450,//★
    Stage2500,//★
    Stage2550,//★
    Stage2600,//★

    Stage2650,//★
    Stage2700,//★
    Stage2750,//★
    Stage2800,//★

    Level128000,//★
    Level131000,//★
    Level134000,//★
    Level137000,//★
    Level140000,//★
    Level143000,//★
    Level146000,//★
    Level149000,//★
    Level152000,//★
    Level155000,//★
    Level158000,//★
    Level161000,//★
    Yomul8,
    Stage2850,//★
    Stage2900,//★
    Stage2950,//★
    Stage3000,//★
    Level164000,//★
    Level167000,//★
    Level170000,//★
    Level173000,//★
    Stage3050,//★
    Stage3100,//★
    Stage3150,//★
    Stage3200,//★
              //
    Level176000,//★
    Level179000,//★
    Level182000,//★
    Level185000,//★
    Level188000,//★
    Level191000,//★
    Level194000,//★
    Level197000,//★
    Level200000,//★
    Level203000,//★
    Level206000,//★
    Level209000,//★
    Level212000,//★
    Level215000,//★
    Level218000,//★
    Level221000,//★
    Level224000,//★
    Level227000,//★
    Level230000,//★
    Level233000,//★
    Level236000,//★
    Level239000,//★
    Level242000,//★
    Level245000,//★
    Level248000,//★
    Level251000,//★

    Stage3250,//★
    Stage3300,//★
    Stage3350,//★
    Stage3400,//★
    Stage3450,//★
    Stage3500,//★
    Stage3550,//★
    Stage3600,//★

    Level255000,//★
    Level260000,//★
    Level265000,//★
    Level270000,//★
    Level275000,//★
    Level280000,//★
    Level285000,//★
    Level290000,//★
    Level295000,//★
    Level300000,//★

    Stage3650,//★
    Stage3700,//★
    Stage3750,//★
    Stage3800,//★
    Stage3850,//★
    Stage3900,//★
    Stage3950,//★
    Stage4000,//★

    Level305000,//★
    Level310000,//★
    Level315000,//★
    Level320000,//★
    Level325000,//★
    Level330000,//★
    Level335000,//★
    Level340000,//★
    Level345000,//★
    Level350000,//★
    GetFeelMulGetEffect,//★

    Stage4050,//★
    Stage4100,//★
    Stage4150,//★
    Stage4200,//★
    Stage4250,//★
    Stage4300,//★
    Stage4350,//★
    Stage4400,//★

    Level355000,//★
    Level360000,//★
    Level365000,//★
    Level370000,//★
    Level375000,//★
    Level380000,//★
    Level385000,//★
    Level390000,//★
    Level395000,//★
    Level400000,//★

    Stage4450,//★
    Stage4500,//★
    Stage4550,//★
    Stage4600,//★
    //
    Stage4650,//★
    Stage4700,//★
    Stage4750,//★
    Stage4800,//★

    Stage4850,//★
    Stage4900,//★
    Stage4950,//★
    Stage5000,//★

    Stage5050,//★
    Stage5100,//★
    Stage5150,//★
    Stage5200,//★


    //
    Level405000,//★
    Level410000,//★
    Level415000,//★
    Level420000,//★
    Level425000,//★
    Level430000,//★
    Level435000,//★
    Level440000,//★
    Level445000,//★
    Level450000,//★
    Level455000,//★
    Level460000,//★
    Level465000,//★
    Level470000,//★
    Level475000,//★
    Level480000,//★
    Level485000,//★
    Level490000,//★
    Level495000,//★
    Level500000,//★

    Stage5250,//★
    Stage5300,//★

    Stage5350,//★
    Stage5400,//★
    Stage5450,//★
    Stage5500,//★
    Stage5550,//★
    Stage5600,//★
    Stage5650,//★
    Stage5700,//★
              //
    Stage5750,//★
    Stage5800,//★
    Stage5850,//★
    Stage5900,//★
    Stage5950,//★
    Stage6000,//★
    //
    Level505000,//★
    Level510000,//★
    Level515000,//★
    Level520000,//★
    Level525000,//★
    Level530000,//★
    Level535000,//★
    Level540000,//★
    Level545000,//★
    Level550000,//★
    Level555000,//★
    Level560000,//★
    Level565000,//★
    Level570000,//★
    Level575000,//★
    Level580000,//★
    Level585000,//★
    Level590000,//★
    Level595000,//★
    Level600000,//★

    Stage6050,//★
    Stage6100,//★
    Stage6150,//★
    Stage6200,//★
    Stage6250,//★
    Stage6300,//★
    Stage6350,//★
    Stage6400,//★
    Stage6450,//★
    Stage6500,//★
    Stage6550,//★
    Stage6600,//★
    Stage6650,//★
    Stage6700,//★
    Stage6750,//★
    Stage6800,//★

    Level605000,//★
    Level610000,//★
    Level615000,//★
    Level620000,//★
    Level625000,//★
    Level630000,//★
    Level635000,//★
    Level640000,//★
    Level645000,//★
    Level650000,//★
    Level655000,//★
    Level660000,//★
    Level665000,//★
    Level670000,//★
    Level675000,//★
    Level680000,//★
    Level685000,//★
    Level690000,//★
    Level695000,//★
    Level700000,//★

    Level705000,//★
    Level710000,//★
    Level715000,//★
    Level720000,//★
    Level725000,//★
    Level730000,//★
    Level735000,//★
    Level740000,//★
    Level745000,//★
    Level750000,//★
    Level755000,//★
    Level760000,//★
    Level765000,//★
    Level770000,//★
    Level775000,//★
    Level780000,//★
    Level785000,//★
    Level790000,//★
    Level795000,//★
    Level800000,//★

    //

    Stage6850,//★
    Stage6900,//★
    Stage6950,//★
    Stage7000,//★
    Stage7050,//★
    Stage7100,//★
    Stage7150,//★
    Stage7200,//★
    //
    Stage7250,//★
    Stage7300,//★
    Stage7350,//★
    Stage7400,//★
    Stage7450,//★
    Stage7500,//★
    Stage7550,//★
    Stage7600,//★
    Stage7650,//★
    Stage7700,//★
    Stage7750,//★
    Stage7800,//★

    Level810000,//★
    Level820000,//★
    Level830000,//★
    Level840000,//★
    Level850000,//★
    Level860000,//★
    Level870000,//★
    Level880000,//★
    Level890000,//★
    Level900000,//★
    Level910000,//★
    Level920000,//★
    Level930000,//★
    Level940000,//★
    Level950000,//★
    Level960000,//★
    Level970000,//★
    Level980000,//★
    Level990000,//★
    Level1000000,//★
    Stage7900,//★
    Stage8000,//★
    Stage8100,//★
    Stage8200,//★

    Stage8300,//★
    Stage8400,//★
    Stage8500,//★
    Stage8600,//★
              //
    GetFeelArm,//★
    GetFeelChun,//★
    GetFeelGuk,//★
    GetIndraWeapon,//★
                   //
    Stage8700,//★
    Stage8800,//★
    Stage8900,//★
    Stage9000,//★
              //
    Level1010000,//★
    Level1020000,//★
    Level1030000,//★
    Level1040000,//★
    Level1050000,//★
    Level1060000,//★
    Level1070000,//★
    Level1080000,//★
    Level1090000,//★
    Level1100000,//★

    Stage9100,//★
    Stage9200,//★
    Stage9300,//★
    Stage9400,//★
              //
    Level1110000,//★
    Level1120000,//★
    Level1130000,//★
    Level1140000,//★
    Level1150000,//★
    Level1160000,//★
    Level1170000,//★
    Level1180000,//★
    Level1190000,//★
    Level1200000,//★
    GetNaTaWeapon,//★
    Stage9500,//★
    Stage9600,//★
    Stage9700,//★
    Stage9800,//★
    Level1210000,//★
    Level1220000,//★
    Level1230000,//★
    Level1240000,//★
    Level1250000,//★
    Level1260000,//★
    Level1270000,//★
    Level1280000,//★
    Level1290000,//★
    Level1300000,//★
    GetOrochiWeapon,//★
    Stage9900,//★
    Stage10000,//★
    FeelMulPaeWeapon,//★

    Stage10100,//★
    Stage10200,//★
    Stage10300,//★
    Stage10400,//★

    //
    Level1310000_Bonus,//★
    Level1310000,//★
    Level1320000,//★
    Level1330000,//★
    Level1340000,//★
    Level1350000,//★
    Level1360000,//★
    Level1370000,//★
    Level1380000,//★
    Level1390000,//★
    Level1400000,//★

    FoxWeapon,//★

    Stage10500,//★
    Stage10600,//★

    Level1410000,//★
    Level1420000,//★
    Level1430000,//★
    Level1440000,//★
    Level1450000,//★
    Level1460000,//★
    Level1470000,//★
    Level1480000,//★
    Level1490000,//★
    Level1500000,//★

    Stage10700,//★
    Stage10800,//★

    HellWeapon,//★

    Stage10900,//★
    Stage11000,//★
    Stage11100,//★
    Stage11200,//★
               //
    Level1510000,//★
    Level1520000,//★
    Level1530000,//★
    Level1540000,//★
    Level1550000,//★
    Level1560000,//★
    Level1570000,//★
    Level1580000,//★
    Level1590000,//★
    Level1600000,//★

    HellWeapon2,//★
    YeoRaeWeapon,//★
    GangRimWeapon,//★
    Stage11300,//★
    Stage11400,//★

    Level1610000,//★
    Level1620000,//★
    Level1630000,//★
    Level1640000,//★
    Level1650000,//★
    Level1660000,//★
    Level1670000,//★
    Level1680000,//★
    Level1690000,//★
    Level1700000,//★
    BulWeaponWeapon,//★
    Level1710000,//★
    Level1720000,//★
    Level1730000,//★
    Level1740000,//★
    Level1750000,//★
    Level1760000,//★
    Level1770000,//★
    Level1780000,//★
    Level1790000,//★
    Level1800000,//★
    Stage11500,//★
    Stage11600,//★
    Stage11700,//★
    Stage11800,//★
    Stage11900,//★
    Stage12000,//★
               //
    Level1810000,//★
    Level1820000,//★
    Level1830000,//★
    Level1840000,//★
    Level1850000,//★
    Level1860000,//★
    Level1870000,//★
    Level1880000,//★
    Level1890000,//★
    Level1900000,//★
                 //
    Level1910000,//★
    Level1920000,//★
    Level1930000,//★
    Level1940000,//★
    Level1950000,//★
    Level1960000,//★
    Level1970000,//★
    Level1980000,//★
    Level1990000,//★
    Level2000000,//★
                 //
    Level2010000,//★
    Level2020000,//★
    Level2030000,//★
    Level2040000,//★
    Level2050000,//★
    Level2060000,//★
    Level2070000,//★
    Level2080000,//★
    Level2090000,//★
    Level2100000,//★
    SainWeapon,//★
    Stage12100,//★
    Stage12200,//★
    Stage12300,//★
    Stage12400,//★

    Stage12500,//★
    Stage12600,//★

    Stage12700,//★
    Stage12800,//★
 
    ChunWeapon0,//★
    ChunWeapon1,//★

    Stage12900,//★
    Stage13000,//★

    Level2110000,//★
    Level2120000,//★
    Level2130000,//★
    Level2140000,//★
    Level2150000,//★
    Level2160000,//★
    Level2170000,//★
    Level2180000,//★
    Level2190000,//★
    Level2200000,//★


    Level2210000,//★
    Level2220000,//★
    Level2230000,//★
    Level2240000,//★
    Level2250000,//★
    Level2260000,//★
    Level2270000,//★
    Level2280000,//★
    Level2290000,//★
    Level2300000,//★

    Stage13100,//★
    Stage13200,//★

    ChunWeapon2,//★
    ChunWeapon3,//★
    

    DokebiWeapon0,//★
    DokebiWeapon1,//★
    DokebiWeapon2,//★


    Stage13300,//★
    Stage13400,//★
    Level2310000,//★
    Level2320000,//★
    Level2330000,//★
    Level2340000,//★
    Level2350000,//★
    Level2360000,//★
    Level2370000,//★
    Level2380000,//★
    Level2390000,//★
    Level2400000,//★


    Level2410000,//★
    Level2420000,//★
    Level2430000,//★
    Level2440000,//★
    Level2450000,//★
    Level2460000,//★
    Level2470000,//★
    Level2480000,//★
    Level2490000,//★
    Level2500000,//★


    Stage13500,//★
    Stage13600,//★

    DokebiWeapon3,//★
    DokebiWeapon4,//★


    Level2510000,//★
    Level2520000,//★
    Level2530000,//★
    Level2540000,//★
    Level2550000,//★
    Level2560000,//★
    Level2570000,//★
    Level2580000,//★
    Level2590000,//★
    Level2600000,//★


    DokebiWeapon5,//★
    DokebiWeapon6,//★


    Stage13700,//★
    Stage13800,//★



    Level2610000,//★
    Level2620000,//★
    Level2630000,//★
    Level2640000,//★
    Level2650000,//★
    Level2660000,//★
    Level2670000,//★
    Level2680000,//★
    Level2690000,//★
    Level2700000,//★



    Stage13900,//★
    Stage14000,//★

    DokebiWeapon7,//★
    DokebiWeapon8,//★
    DokebiWeapon9,//★
    




    Stage14100,//★
    Stage14200,//★



    Level2710000,//★
    Level2720000,//★
    Level2730000,//★
    Level2740000,//★
    Level2750000,//★
    Level2760000,//★
    Level2770000,//★
    Level2780000,//★
    Level2790000,//★
    Level2800000,//★

    SumisanWeapon0,//★




    Stage14300,//★
    Stage14400,//★


    Level2810000,//★
    Level2820000,//★
    Level2830000,//★
    Level2840000,//★
    Level2850000,//★
    Level2860000,//★
    Level2870000,//★
    Level2880000,//★
    Level2890000,//★
    Level2900000,//★
    
    SumisanWeapon1,//★



    Level2910000,//★
    Level2920000,//★
    Level2930000,//★
    Level2940000,//★
    Level2950000,//★
    Level2960000,//★
    Level2970000,//★
    Level2980000,//★
    Level2990000,//★
    Level3000000,//★



    Stage14500,//★
    Stage14600,//★
    
    Level3010000,//★
    Level3020000,//★
    Level3030000,//★
    Level3040000,//★
    Level3050000,//★
    Level3060000,//★
    Level3070000,//★
    Level3080000,//★
    Level3090000,//★
    Level3100000,//★

    Stage14700,//★
    Stage14800,//★

    SumisanWeapon2,//★
    SumisanWeapon3,//★



    Stage14900,//★
    Stage15000,//★

    Level3110000,//★
    Level3120000,//★
    Level3130000,//★
    Level3140000,//★
    Level3150000,//★
    Level3160000,//★
    Level3170000,//★
    Level3180000,//★
    Level3190000,//★
    Level3200000,//★
}
public class UiTitleManager : SingletonMono<UiTitleManager>
{
    private void Start()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        ServerData.statusTable.GetTableData(StatusTable.Level).AsObservable().Subscribe(e =>
        {

            if (e >= 1000)
            {
                ClearTitleMission(TitleMissionId.Level1000);
            }

            if (e >= 2000)
            {
                ClearTitleMission(TitleMissionId.Level2000);
            }

            if (e >= 3000)
            {
                ClearTitleMission(TitleMissionId.Level3000);
            }

            if (e >= 5000)
            {
                ClearTitleMission(TitleMissionId.Level5000);
            }

            if (e >= 7000)
            {
                ClearTitleMission(TitleMissionId.Level7000);
            }

            if (e >= 9000)
            {
                ClearTitleMission(TitleMissionId.Level9000);
            }

            if (e >= 11000)
            {
                ClearTitleMission(TitleMissionId.Level11000);
            }

            if (e >= 13000)
            {
                ClearTitleMission(TitleMissionId.Level13000);
            }

            if (e >= 15000)
            {
                ClearTitleMission(TitleMissionId.Level15000);
            }

            if (e >= 17000)
            {
                ClearTitleMission(TitleMissionId.Level17000);
            }

            if (e >= 20000)
            {
                ClearTitleMission(TitleMissionId.Level20000);
            }

            if (e >= 23000)
            {
                ClearTitleMission(TitleMissionId.Level23000);
            }

            if (e >= 26000)
            {
                ClearTitleMission(TitleMissionId.Level26000);
            }

            if (e >= 29000)
            {
                ClearTitleMission(TitleMissionId.Level29000);
            }

            if (e >= 32000)
            {
                ClearTitleMission(TitleMissionId.Level32000);
            }

            if (e >= 35000)
            {
                ClearTitleMission(TitleMissionId.Level35000);
            }

            if (e >= 38000)
            {
                ClearTitleMission(TitleMissionId.Level38000);
            }

            if (e >= 41000)
            {
                ClearTitleMission(TitleMissionId.Level41000);
            }

            if (e >= 44000)
            {
                ClearTitleMission(TitleMissionId.Level44000);
            }

            if (e >= 47000)
            {
                ClearTitleMission(TitleMissionId.Level47000);
            }

            if (e >= 50000)
            {
                ClearTitleMission(TitleMissionId.Level50000);
            }

            if (e >= 53000)
            {
                ClearTitleMission(TitleMissionId.Level53000);
            }

            /////////
            ///
            if (e >= 56000)
            {
                ClearTitleMission(TitleMissionId.Level56000);
            }
            if (e >= 59000)
            {
                ClearTitleMission(TitleMissionId.Level59000);
            }
            if (e >= 62000)
            {
                ClearTitleMission(TitleMissionId.Level62000);
            }
            if (e >= 65000)
            {
                ClearTitleMission(TitleMissionId.Level65000);
            }
            if (e >= 68000)
            {
                ClearTitleMission(TitleMissionId.Level68000);
            }
            if (e >= 71000)
            {
                ClearTitleMission(TitleMissionId.Level71000);
            }
            if (e >= 74000)
            {
                ClearTitleMission(TitleMissionId.Level74000);
            }
            if (e >= 77000)
            {
                ClearTitleMission(TitleMissionId.Level77000);
            }
            if (e >= 80000)
            {
                ClearTitleMission(TitleMissionId.Level80000);
            }
            if (e >= 83000)
            {
                ClearTitleMission(TitleMissionId.Level83000);
            }

            //
            if (e >= 86000)
            {
                ClearTitleMission(TitleMissionId.Level86000);
            }
            if (e >= 89000)
            {
                ClearTitleMission(TitleMissionId.Level89000);
            }
            if (e >= 92000)
            {
                ClearTitleMission(TitleMissionId.Level92000);
            }
            if (e >= 95000)
            {
                ClearTitleMission(TitleMissionId.Level95000);
            }
            if (e >= 98000)
            {
                ClearTitleMission(TitleMissionId.Level98000);
            }
            if (e >= 101000)
            {
                ClearTitleMission(TitleMissionId.Level101000);
            }

            //
            if (e >= 104000)
            {
                ClearTitleMission(TitleMissionId.Level104000);
            }
            if (e >= 107000)
            {
                ClearTitleMission(TitleMissionId.Level107000);
            }
            if (e >= 110000)
            {
                ClearTitleMission(TitleMissionId.Level110000);
            }
            if (e >= 113000)
            {
                ClearTitleMission(TitleMissionId.Level113000);
            }
            if (e >= 116000)
            {
                ClearTitleMission(TitleMissionId.Level116000);
            }
            if (e >= 119000)
            {
                ClearTitleMission(TitleMissionId.Level119000);
            }
            if (e >= 122000)
            {
                ClearTitleMission(TitleMissionId.Level122000);
            }
            if (e >= 125000)
            {
                ClearTitleMission(TitleMissionId.Level125000);
            }

            //
            if (e >= 128000)
            {
                ClearTitleMission(TitleMissionId.Level128000);
            }
            if (e >= 131000)
            {
                ClearTitleMission(TitleMissionId.Level131000);
            }
            if (e >= 134000)
            {
                ClearTitleMission(TitleMissionId.Level134000);
            }
            if (e >= 137000)
            {
                ClearTitleMission(TitleMissionId.Level137000);
            }
            if (e >= 140000)
            {
                ClearTitleMission(TitleMissionId.Level140000);
            }
            if (e >= 143000)
            {
                ClearTitleMission(TitleMissionId.Level143000);
            }
            if (e >= 146000)
            {
                ClearTitleMission(TitleMissionId.Level146000);
            }
            if (e >= 149000)
            {
                ClearTitleMission(TitleMissionId.Level149000);
            }
            if (e >= 152000)
            {
                ClearTitleMission(TitleMissionId.Level152000);
            }
            if (e >= 155000)
            {
                ClearTitleMission(TitleMissionId.Level155000);
            }
            if (e >= 158000)
            {
                ClearTitleMission(TitleMissionId.Level158000);
            }
            if (e >= 161000)
            {
                ClearTitleMission(TitleMissionId.Level161000);
            }
            //
            if (e >= 164000)
            {
                ClearTitleMission(TitleMissionId.Level164000);
            }
            if (e >= 167000)
            {
                ClearTitleMission(TitleMissionId.Level167000);
            }
            if (e >= 170000)
            {
                ClearTitleMission(TitleMissionId.Level170000);
            }
            if (e >= 173000)
            {
                ClearTitleMission(TitleMissionId.Level173000);
            }
            //

            if (e >= 176000)
            {
                ClearTitleMission(TitleMissionId.Level176000);
            }
            if (e >= 179000)
            {
                ClearTitleMission(TitleMissionId.Level179000);
            }
            if (e >= 182000)
            {
                ClearTitleMission(TitleMissionId.Level182000);
            }
            if (e >= 185000)
            {
                ClearTitleMission(TitleMissionId.Level185000);
            }
            if (e >= 188000)
            {
                ClearTitleMission(TitleMissionId.Level188000);
            }
            if (e >= 191000)
            {
                ClearTitleMission(TitleMissionId.Level191000);
            }
            if (e >= 194000)
            {
                ClearTitleMission(TitleMissionId.Level194000);
            }
            if (e >= 197000)
            {
                ClearTitleMission(TitleMissionId.Level197000);
            }
            if (e >= 200000)
            {
                ClearTitleMission(TitleMissionId.Level200000);
            }
            if (e >= 203000)
            {
                ClearTitleMission(TitleMissionId.Level203000);
            }
            if (e >= 206000)
            {
                ClearTitleMission(TitleMissionId.Level206000);
            }
            if (e >= 209000)
            {
                ClearTitleMission(TitleMissionId.Level209000);
            }
            if (e >= 212000)
            {
                ClearTitleMission(TitleMissionId.Level212000);
            }
            if (e >= 215000)
            {
                ClearTitleMission(TitleMissionId.Level215000);
            }
            if (e >= 218000)
            {
                ClearTitleMission(TitleMissionId.Level218000);
            }
            if (e >= 221000)
            {
                ClearTitleMission(TitleMissionId.Level221000);
            }
            if (e >= 224000)
            {
                ClearTitleMission(TitleMissionId.Level224000);
            }
            if (e >= 227000)
            {
                ClearTitleMission(TitleMissionId.Level227000);
            }
            if (e >= 230000)
            {
                ClearTitleMission(TitleMissionId.Level230000);
            }
            if (e >= 233000)
            {
                ClearTitleMission(TitleMissionId.Level233000);
            }
            if (e >= 236000)
            {
                ClearTitleMission(TitleMissionId.Level236000);
            }
            if (e >= 239000)
            {
                ClearTitleMission(TitleMissionId.Level239000);
            }
            if (e >= 242000)
            {
                ClearTitleMission(TitleMissionId.Level242000);
            }
            if (e >= 245000)
            {
                ClearTitleMission(TitleMissionId.Level245000);
            }
            if (e >= 248000)
            {
                ClearTitleMission(TitleMissionId.Level248000);
            }
            if (e >= 251000)
            {
                ClearTitleMission(TitleMissionId.Level251000);
            }

            if (e >= 255000)
            {
                ClearTitleMission(TitleMissionId.Level255000);
            }
            if (e >= 260000)
            {
                ClearTitleMission(TitleMissionId.Level260000);
            }
            if (e >= 265000)
            {
                ClearTitleMission(TitleMissionId.Level265000);
            }
            if (e >= 270000)
            {
                ClearTitleMission(TitleMissionId.Level270000);
            }
            if (e >= 275000)
            {
                ClearTitleMission(TitleMissionId.Level275000);
            }
            if (e >= 280000)
            {
                ClearTitleMission(TitleMissionId.Level280000);
            }
            if (e >= 285000)
            {
                ClearTitleMission(TitleMissionId.Level285000);
            }
            if (e >= 290000)
            {
                ClearTitleMission(TitleMissionId.Level290000);
            }
            if (e >= 295000)
            {
                ClearTitleMission(TitleMissionId.Level295000);
            }
            if (e >= 300000)
            {
                ClearTitleMission(TitleMissionId.Level300000);
            }

            //

            if (e >= 305000)
            {
                ClearTitleMission(TitleMissionId.Level305000);
            }
            if (e >= 310000)
            {
                ClearTitleMission(TitleMissionId.Level310000);
            }
            if (e >= 315000)
            {
                ClearTitleMission(TitleMissionId.Level315000);
            }
            if (e >= 320000)
            {
                ClearTitleMission(TitleMissionId.Level320000);
            }
            if (e >= 325000)
            {
                ClearTitleMission(TitleMissionId.Level325000);
            }
            if (e >= 330000)
            {
                ClearTitleMission(TitleMissionId.Level330000);
            }
            if (e >= 335000)
            {
                ClearTitleMission(TitleMissionId.Level335000);
            }
            if (e >= 340000)
            {
                ClearTitleMission(TitleMissionId.Level340000);
            }
            if (e >= 345000)
            {
                ClearTitleMission(TitleMissionId.Level345000);
            }
            if (e >= 350000)
            {
                ClearTitleMission(TitleMissionId.Level350000);
            }

            //

            if (e >= 355000)
            {
                ClearTitleMission(TitleMissionId.Level355000);
            }
            if (e >= 360000)
            {
                ClearTitleMission(TitleMissionId.Level360000);
            }
            if (e >= 365000)
            {
                ClearTitleMission(TitleMissionId.Level365000);
            }
            if (e >= 370000)
            {
                ClearTitleMission(TitleMissionId.Level370000);
            }
            if (e >= 375000)
            {
                ClearTitleMission(TitleMissionId.Level375000);
            }
            if (e >= 380000)
            {
                ClearTitleMission(TitleMissionId.Level380000);
            }
            if (e >= 385000)
            {
                ClearTitleMission(TitleMissionId.Level385000);
            }
            if (e >= 390000)
            {
                ClearTitleMission(TitleMissionId.Level390000);
            }
            if (e >= 395000)
            {
                ClearTitleMission(TitleMissionId.Level395000);
            }
            if (e >= 400000)
            {
                ClearTitleMission(TitleMissionId.Level400000);
            }
            //
            if (e >= 405000)
            {
                ClearTitleMission(TitleMissionId.Level405000);
            }
            if (e >= 410000)
            {
                ClearTitleMission(TitleMissionId.Level410000);
            }
            if (e >= 415000)
            {
                ClearTitleMission(TitleMissionId.Level415000);
            }
            if (e >= 420000)
            {
                ClearTitleMission(TitleMissionId.Level420000);
            }
            if (e >= 425000)
            {
                ClearTitleMission(TitleMissionId.Level425000);
            }
            if (e >= 430000)
            {
                ClearTitleMission(TitleMissionId.Level430000);
            }
            if (e >= 435000)
            {
                ClearTitleMission(TitleMissionId.Level435000);
            }
            if (e >= 440000)
            {
                ClearTitleMission(TitleMissionId.Level440000);
            }
            if (e >= 445000)
            {
                ClearTitleMission(TitleMissionId.Level445000);
            }
            if (e >= 450000)
            {
                ClearTitleMission(TitleMissionId.Level450000);
            }
            //
            if (e >= 455000)
            {
                ClearTitleMission(TitleMissionId.Level455000);
            }
            if (e >= 460000)
            {
                ClearTitleMission(TitleMissionId.Level460000);
            }
            if (e >= 465000)
            {
                ClearTitleMission(TitleMissionId.Level465000);
            }
            if (e >= 470000)
            {
                ClearTitleMission(TitleMissionId.Level470000);
            }
            if (e >= 475000)
            {
                ClearTitleMission(TitleMissionId.Level475000);
            }
            if (e >= 480000)
            {
                ClearTitleMission(TitleMissionId.Level480000);
            }
            if (e >= 485000)
            {
                ClearTitleMission(TitleMissionId.Level485000);
            }
            if (e >= 490000)
            {
                ClearTitleMission(TitleMissionId.Level490000);
            }
            if (e >= 495000)
            {
                ClearTitleMission(TitleMissionId.Level495000);
            }
            if (e >= 500000)
            {
                ClearTitleMission(TitleMissionId.Level500000);
            }
            //
            if (e >= 505000)
            {
                ClearTitleMission(TitleMissionId.Level505000);
            }
            if (e >= 510000)
            {
                ClearTitleMission(TitleMissionId.Level510000);
            }
            if (e >= 515000)
            {
                ClearTitleMission(TitleMissionId.Level515000);
            }
            if (e >= 520000)
            {
                ClearTitleMission(TitleMissionId.Level520000);
            }
            if (e >= 525000)
            {
                ClearTitleMission(TitleMissionId.Level525000);
            }
            if (e >= 530000)
            {
                ClearTitleMission(TitleMissionId.Level530000);
            }
            if (e >= 535000)
            {
                ClearTitleMission(TitleMissionId.Level535000);
            }
            if (e >= 540000)
            {
                ClearTitleMission(TitleMissionId.Level540000);
            }
            if (e >= 545000)
            {
                ClearTitleMission(TitleMissionId.Level545000);
            }
            if (e >= 550000)
            {
                ClearTitleMission(TitleMissionId.Level550000);
            }
            //
            if (e >= 555000)
            {
                ClearTitleMission(TitleMissionId.Level555000);
            }
            if (e >= 560000)
            {
                ClearTitleMission(TitleMissionId.Level560000);
            }
            if (e >= 565000)
            {
                ClearTitleMission(TitleMissionId.Level565000);
            }
            if (e >= 570000)
            {
                ClearTitleMission(TitleMissionId.Level570000);
            }
            if (e >= 575000)
            {
                ClearTitleMission(TitleMissionId.Level575000);
            }
            if (e >= 580000)
            {
                ClearTitleMission(TitleMissionId.Level580000);
            }
            if (e >= 585000)
            {
                ClearTitleMission(TitleMissionId.Level585000);
            }
            if (e >= 590000)
            {
                ClearTitleMission(TitleMissionId.Level590000);
            }
            if (e >= 595000)
            {
                ClearTitleMission(TitleMissionId.Level595000);
            }
            if (e >= 600000)
            {
                ClearTitleMission(TitleMissionId.Level600000);
            }
            /////
            ///

            if (e >= 605000)
            {
                ClearTitleMission(TitleMissionId.Level605000);
            }
            if (e >= 610000)
            {
                ClearTitleMission(TitleMissionId.Level610000);
            }
            if (e >= 615000)
            {
                ClearTitleMission(TitleMissionId.Level615000);
            }
            if (e >= 620000)
            {
                ClearTitleMission(TitleMissionId.Level620000);
            }
            if (e >= 625000)
            {
                ClearTitleMission(TitleMissionId.Level625000);
            }
            if (e >= 630000)
            {
                ClearTitleMission(TitleMissionId.Level630000);
            }
            if (e >= 635000)
            {
                ClearTitleMission(TitleMissionId.Level635000);
            }
            if (e >= 640000)
            {
                ClearTitleMission(TitleMissionId.Level640000);
            }
            if (e >= 645000)
            {
                ClearTitleMission(TitleMissionId.Level645000);
            }
            if (e >= 650000)
            {
                ClearTitleMission(TitleMissionId.Level650000);
            }
            //
            if (e >= 655000)
            {
                ClearTitleMission(TitleMissionId.Level655000);
            }
            if (e >= 660000)
            {
                ClearTitleMission(TitleMissionId.Level660000);
            }
            if (e >= 665000)
            {
                ClearTitleMission(TitleMissionId.Level665000);
            }
            if (e >= 670000)
            {
                ClearTitleMission(TitleMissionId.Level670000);
            }
            if (e >= 675000)
            {
                ClearTitleMission(TitleMissionId.Level675000);
            }
            if (e >= 680000)
            {
                ClearTitleMission(TitleMissionId.Level680000);
            }
            if (e >= 685000)
            {
                ClearTitleMission(TitleMissionId.Level685000);
            }
            if (e >= 690000)
            {
                ClearTitleMission(TitleMissionId.Level690000);
            }
            if (e >= 695000)
            {
                ClearTitleMission(TitleMissionId.Level695000);
            }
            if (e >= 700000)
            {
                ClearTitleMission(TitleMissionId.Level700000);
            }

            /////////////////////
            ///
            if (e >= 705000)
            {
                ClearTitleMission(TitleMissionId.Level705000);
            }
            if (e >= 710000)
            {
                ClearTitleMission(TitleMissionId.Level710000);
            }
            if (e >= 715000)
            {
                ClearTitleMission(TitleMissionId.Level715000);
            }
            if (e >= 720000)
            {
                ClearTitleMission(TitleMissionId.Level720000);
            }
            if (e >= 725000)
            {
                ClearTitleMission(TitleMissionId.Level725000);
            }
            if (e >= 730000)
            {
                ClearTitleMission(TitleMissionId.Level730000);
            }
            if (e >= 735000)
            {
                ClearTitleMission(TitleMissionId.Level735000);
            }
            if (e >= 740000)
            {
                ClearTitleMission(TitleMissionId.Level740000);
            }
            if (e >= 745000)
            {
                ClearTitleMission(TitleMissionId.Level745000);
            }
            if (e >= 750000)
            {
                ClearTitleMission(TitleMissionId.Level750000);
            }
            //
            if (e >= 755000)
            {
                ClearTitleMission(TitleMissionId.Level755000);
            }
            if (e >= 760000)
            {
                ClearTitleMission(TitleMissionId.Level760000);
            }
            if (e >= 765000)
            {
                ClearTitleMission(TitleMissionId.Level765000);
            }
            if (e >= 770000)
            {
                ClearTitleMission(TitleMissionId.Level770000);
            }
            if (e >= 775000)
            {
                ClearTitleMission(TitleMissionId.Level775000);
            }
            if (e >= 780000)
            {
                ClearTitleMission(TitleMissionId.Level780000);
            }
            if (e >= 785000)
            {
                ClearTitleMission(TitleMissionId.Level785000);
            }
            if (e >= 790000)
            {
                ClearTitleMission(TitleMissionId.Level790000);
            }
            if (e >= 795000)
            {
                ClearTitleMission(TitleMissionId.Level795000);
            }
            if (e >= 800000)
            {
                ClearTitleMission(TitleMissionId.Level800000);
            }
            //
            if (e >= 810000)
            {
                ClearTitleMission(TitleMissionId.Level810000);
            }
            if (e >= 820000)
            {
                ClearTitleMission(TitleMissionId.Level820000);
            }
            if (e >= 830000)
            {
                ClearTitleMission(TitleMissionId.Level830000);
            }
            if (e >= 840000)
            {
                ClearTitleMission(TitleMissionId.Level840000);
            }
            if (e >= 850000)
            {
                ClearTitleMission(TitleMissionId.Level850000);
            }
            if (e >= 860000)
            {
                ClearTitleMission(TitleMissionId.Level860000);
            }
            if (e >= 870000)
            {
                ClearTitleMission(TitleMissionId.Level870000);
            }
            if (e >= 880000)
            {
                ClearTitleMission(TitleMissionId.Level880000);
            }
            if (e >= 890000)
            {
                ClearTitleMission(TitleMissionId.Level890000);
            }
            if (e >= 900000)
            {
                ClearTitleMission(TitleMissionId.Level900000);
            }
            if (e >= 910000)
            {
                ClearTitleMission(TitleMissionId.Level910000);
            }
            if (e >= 920000)
            {
                ClearTitleMission(TitleMissionId.Level920000);
            }
            if (e >= 930000)
            {
                ClearTitleMission(TitleMissionId.Level930000);
            }
            if (e >= 940000)
            {
                ClearTitleMission(TitleMissionId.Level940000);
            }
            if (e >= 950000)
            {
                ClearTitleMission(TitleMissionId.Level950000);
            }
            if (e >= 960000)
            {
                ClearTitleMission(TitleMissionId.Level960000);
            }
            if (e >= 970000)
            {
                ClearTitleMission(TitleMissionId.Level970000);
            }
            if (e >= 980000)
            {
                ClearTitleMission(TitleMissionId.Level980000);
            }
            if (e >= 990000)
            {
                ClearTitleMission(TitleMissionId.Level990000);
            }
            if (e >= 1000000)
            {
                ClearTitleMission(TitleMissionId.Level1000000);
            }
            //
            if (e >= 1010000)
            {
                ClearTitleMission(TitleMissionId.Level1010000);
            }
            if (e >= 1020000)
            {
                ClearTitleMission(TitleMissionId.Level1020000);
            }
            if (e >= 1030000)
            {
                ClearTitleMission(TitleMissionId.Level1030000);
            }
            if (e >= 1040000)
            {
                ClearTitleMission(TitleMissionId.Level1040000);
            }
            if (e >= 1050000)
            {
                ClearTitleMission(TitleMissionId.Level1050000);
            }
            if (e >= 1060000)
            {
                ClearTitleMission(TitleMissionId.Level1060000);
            }
            if (e >= 1070000)
            {
                ClearTitleMission(TitleMissionId.Level1070000);
            }
            if (e >= 1080000)
            {
                ClearTitleMission(TitleMissionId.Level1080000);
            }
            if (e >= 1090000)
            {
                ClearTitleMission(TitleMissionId.Level1090000);
            }
            if (e >= 1100000)
            {
                ClearTitleMission(TitleMissionId.Level1100000);
            }
            //
            if (e >= 1110000)
            {
                ClearTitleMission(TitleMissionId.Level1110000);
            }
            if (e >= 1120000)
            {
                ClearTitleMission(TitleMissionId.Level1120000);
            }
            if (e >= 1130000)
            {
                ClearTitleMission(TitleMissionId.Level1130000);
            }
            if (e >= 1140000)
            {
                ClearTitleMission(TitleMissionId.Level1140000);
            }
            if (e >= 1150000)
            {
                ClearTitleMission(TitleMissionId.Level1150000);
            }
            if (e >= 1160000)
            {
                ClearTitleMission(TitleMissionId.Level1160000);
            }
            if (e >= 1170000)
            {
                ClearTitleMission(TitleMissionId.Level1170000);
            }
            if (e >= 1180000)
            {
                ClearTitleMission(TitleMissionId.Level1180000);
            }
            if (e >= 1190000)
            {
                ClearTitleMission(TitleMissionId.Level1190000);
            }
            if (e >= 1200000)
            {
                ClearTitleMission(TitleMissionId.Level1200000);
            }
            //
            if (e >= 1210000)
            {
                ClearTitleMission(TitleMissionId.Level1210000);
            }
            if (e >= 1220000)
            {
                ClearTitleMission(TitleMissionId.Level1220000);
            }
            if (e >= 1230000)
            {
                ClearTitleMission(TitleMissionId.Level1230000);
            }
            if (e >= 1240000)
            {
                ClearTitleMission(TitleMissionId.Level1240000);
            }
            if (e >= 1250000)
            {
                ClearTitleMission(TitleMissionId.Level1250000);
            }
            if (e >= 1260000)
            {
                ClearTitleMission(TitleMissionId.Level1260000);
            }
            if (e >= 1270000)
            {
                ClearTitleMission(TitleMissionId.Level1270000);
            }
            if (e >= 1280000)
            {
                ClearTitleMission(TitleMissionId.Level1280000);
            }
            if (e >= 1290000)
            {
                ClearTitleMission(TitleMissionId.Level1290000);
            }
            if (e >= 1300000)
            {
                ClearTitleMission(TitleMissionId.Level1300000);
            }

            //
            if (e >= 1310000)
            {
                ClearTitleMission(TitleMissionId.Level1310000_Bonus);
                ClearTitleMission(TitleMissionId.Level1310000);
            }
            if (e >= 1320000)
            {
                ClearTitleMission(TitleMissionId.Level1320000);
            }
            if (e >= 1330000)
            {
                ClearTitleMission(TitleMissionId.Level1330000);
            }
            if (e >= 1340000)
            {
                ClearTitleMission(TitleMissionId.Level1340000);
            }
            if (e >= 1350000)
            {
                ClearTitleMission(TitleMissionId.Level1350000);
            }
            if (e >= 1360000)
            {
                ClearTitleMission(TitleMissionId.Level1360000);
            }
            if (e >= 1370000)
            {
                ClearTitleMission(TitleMissionId.Level1370000);
            }
            if (e >= 1380000)
            {
                ClearTitleMission(TitleMissionId.Level1380000);
            }
            if (e >= 1390000)
            {
                ClearTitleMission(TitleMissionId.Level1390000);
            }
            if (e >= 1400000)
            {
                ClearTitleMission(TitleMissionId.Level1400000);
            }
            //


            if (e >= 1410000)
            {
                ClearTitleMission(TitleMissionId.Level1410000);
            }
            if (e >= 1420000)
            {
                ClearTitleMission(TitleMissionId.Level1420000);
            }
            if (e >= 1430000)
            {
                ClearTitleMission(TitleMissionId.Level1430000);
            }
            if (e >= 1440000)
            {
                ClearTitleMission(TitleMissionId.Level1440000);
            }
            if (e >= 1450000)
            {
                ClearTitleMission(TitleMissionId.Level1450000);
            }
            if (e >= 1460000)
            {
                ClearTitleMission(TitleMissionId.Level1460000);
            }
            if (e >= 1470000)
            {
                ClearTitleMission(TitleMissionId.Level1470000);
            }
            if (e >= 1480000)
            {
                ClearTitleMission(TitleMissionId.Level1480000);
            }
            if (e >= 1490000)
            {
                ClearTitleMission(TitleMissionId.Level1490000);
            }
            if (e >= 1500000)
            {
                ClearTitleMission(TitleMissionId.Level1500000);
            }
            //
            if (e >= 1510000)
            {
                ClearTitleMission(TitleMissionId.Level1510000);
            }
            if (e >= 1520000)
            {
                ClearTitleMission(TitleMissionId.Level1520000);
            }
            if (e >= 1530000)
            {
                ClearTitleMission(TitleMissionId.Level1530000);
            }
            if (e >= 1540000)
            {
                ClearTitleMission(TitleMissionId.Level1540000);
            }
            if (e >= 1550000)
            {
                ClearTitleMission(TitleMissionId.Level1550000);
            }
            if (e >= 1560000)
            {
                ClearTitleMission(TitleMissionId.Level1560000);
            }
            if (e >= 1570000)
            {
                ClearTitleMission(TitleMissionId.Level1570000);
            }
            if (e >= 1580000)
            {
                ClearTitleMission(TitleMissionId.Level1580000);
            }
            if (e >= 1590000)
            {
                ClearTitleMission(TitleMissionId.Level1590000);
            }
            if (e >= 1600000)
            {
                ClearTitleMission(TitleMissionId.Level1600000);
            }
            //
            if (e >= 1610000)
            {
                ClearTitleMission(TitleMissionId.Level1610000);
            }
            if (e >= 1620000)
            {
                ClearTitleMission(TitleMissionId.Level1620000);
            }
            if (e >= 1630000)
            {
                ClearTitleMission(TitleMissionId.Level1630000);
            }
            if (e >= 1640000)
            {
                ClearTitleMission(TitleMissionId.Level1640000);
            }
            if (e >= 1650000)
            {
                ClearTitleMission(TitleMissionId.Level1650000);
            }
            if (e >= 1660000)
            {
                ClearTitleMission(TitleMissionId.Level1660000);
            }
            if (e >= 1670000)
            {
                ClearTitleMission(TitleMissionId.Level1670000);
            }
            if (e >= 1680000)
            {
                ClearTitleMission(TitleMissionId.Level1680000);
            }
            if (e >= 1690000)
            {
                ClearTitleMission(TitleMissionId.Level1690000);
            }
            if (e >= 1700000)
            {
                ClearTitleMission(TitleMissionId.Level1700000);
            }
            //
            if (e >= 1710000)
            {
                ClearTitleMission(TitleMissionId.Level1710000);
            }
            if (e >= 1720000)
            {
                ClearTitleMission(TitleMissionId.Level1720000);
            }
            if (e >= 1730000)
            {
                ClearTitleMission(TitleMissionId.Level1730000);
            }
            if (e >= 1740000)
            {
                ClearTitleMission(TitleMissionId.Level1740000);
            }
            if (e >= 1750000)
            {
                ClearTitleMission(TitleMissionId.Level1750000);
            }
            if (e >= 1760000)
            {
                ClearTitleMission(TitleMissionId.Level1760000);
            }
            if (e >= 1770000)
            {
                ClearTitleMission(TitleMissionId.Level1770000);
            }
            if (e >= 1780000)
            {
                ClearTitleMission(TitleMissionId.Level1780000);
            }
            if (e >= 1790000)
            {
                ClearTitleMission(TitleMissionId.Level1790000);
            }
            if (e >= 1800000)
            {
                ClearTitleMission(TitleMissionId.Level1800000);
            }
            //
            if (e >= 1810000)
            {
                ClearTitleMission(TitleMissionId.Level1810000);
            }
            if (e >= 1820000)
            {
                ClearTitleMission(TitleMissionId.Level1820000);
            }
            if (e >= 1830000)
            {
                ClearTitleMission(TitleMissionId.Level1830000);
            }
            if (e >= 1840000)
            {
                ClearTitleMission(TitleMissionId.Level1840000);
            }
            if (e >= 1850000)
            {
                ClearTitleMission(TitleMissionId.Level1850000);
            }
            if (e >= 1860000)
            {
                ClearTitleMission(TitleMissionId.Level1860000);
            }
            if (e >= 1870000)
            {
                ClearTitleMission(TitleMissionId.Level1870000);
            }
            if (e >= 1880000)
            {
                ClearTitleMission(TitleMissionId.Level1880000);
            }
            if (e >= 1890000)
            {
                ClearTitleMission(TitleMissionId.Level1890000);
            }
            if (e >= 1900000)
            {
                ClearTitleMission(TitleMissionId.Level1900000);
            }
            //
            //
            if (e >= 1910000)
            {
                ClearTitleMission(TitleMissionId.Level1910000);
            }
            if (e >= 1920000)
            {
                ClearTitleMission(TitleMissionId.Level1920000);
            }
            if (e >= 1930000)
            {
                ClearTitleMission(TitleMissionId.Level1930000);
            }
            if (e >= 1940000)
            {
                ClearTitleMission(TitleMissionId.Level1940000);
            }
            if (e >= 1950000)
            {
                ClearTitleMission(TitleMissionId.Level1950000);
            }
            if (e >= 1960000)
            {
                ClearTitleMission(TitleMissionId.Level1960000);
            }
            if (e >= 1970000)
            {
                ClearTitleMission(TitleMissionId.Level1970000);
            }
            if (e >= 1980000)
            {
                ClearTitleMission(TitleMissionId.Level1980000);
            }
            if (e >= 1990000)
            {
                ClearTitleMission(TitleMissionId.Level1990000);
            }
            if (e >= 2000000)
            {
                ClearTitleMission(TitleMissionId.Level2000000);
            }
            if (e >= 2010000)
            {
                ClearTitleMission(TitleMissionId.Level2010000);
            }
            if (e >= 2020000)
            {
                ClearTitleMission(TitleMissionId.Level2020000);
            }
            if (e >= 2030000)
            {
                ClearTitleMission(TitleMissionId.Level2030000);
            }
            if (e >= 2040000)
            {
                ClearTitleMission(TitleMissionId.Level2040000);
            }
            if (e >= 2050000)
            {
                ClearTitleMission(TitleMissionId.Level2050000);
            }
            if (e >= 2060000)
            {
                ClearTitleMission(TitleMissionId.Level2060000);
            }
            if (e >= 2070000)
            {
                ClearTitleMission(TitleMissionId.Level2070000);
            }
            if (e >= 2080000)
            {
                ClearTitleMission(TitleMissionId.Level2080000);
            }
            if (e >= 2090000)
            {
                ClearTitleMission(TitleMissionId.Level2090000);
            }
            if (e >= 2100000)
            {
                ClearTitleMission(TitleMissionId.Level2100000);
            }
            //
            if (e >= 2110000)
            {
                ClearTitleMission(TitleMissionId.Level2110000);
            }
            if (e >= 2120000)
            {
                ClearTitleMission(TitleMissionId.Level2120000);
            }
            if (e >= 2130000)
            {
                ClearTitleMission(TitleMissionId.Level2130000);
            }
            if (e >= 2140000)
            {
                ClearTitleMission(TitleMissionId.Level2140000);
            }
            if (e >= 2150000)
            {
                ClearTitleMission(TitleMissionId.Level2150000);
            }
            if (e >= 2160000)
            {
                ClearTitleMission(TitleMissionId.Level2160000);
            }
            if (e >= 2170000)
            {
                ClearTitleMission(TitleMissionId.Level2170000);
            }
            if (e >= 2180000)
            {
                ClearTitleMission(TitleMissionId.Level2180000);
            }
            if (e >= 2190000)
            {
                ClearTitleMission(TitleMissionId.Level2190000);
            }
            if (e >= 2200000)
            {
                ClearTitleMission(TitleMissionId.Level2200000);
            }
            //
            if (e >= 2210000)
            {
                ClearTitleMission(TitleMissionId.Level2210000);
            }
            if (e >= 2220000)
            {
                ClearTitleMission(TitleMissionId.Level2220000);
            }
            if (e >= 2230000)
            {
                ClearTitleMission(TitleMissionId.Level2230000);
            }
            if (e >= 2240000)
            {
                ClearTitleMission(TitleMissionId.Level2240000);
            }
            if (e >= 2250000)
            {
                ClearTitleMission(TitleMissionId.Level2250000);
            }
            if (e >= 2260000)
            {
                ClearTitleMission(TitleMissionId.Level2260000);
            }
            if (e >= 2270000)
            {
                ClearTitleMission(TitleMissionId.Level2270000);
            }
            if (e >= 2280000)
            {
                ClearTitleMission(TitleMissionId.Level2280000);
            }
            if (e >= 2290000)
            {
                ClearTitleMission(TitleMissionId.Level2290000);
            }
            if (e >= 2300000)
            {
                ClearTitleMission(TitleMissionId.Level2300000);
            }
            if (e >= 2310000)
            {
                ClearTitleMission(TitleMissionId.Level2310000);
            }
            if (e >= 2320000)
            {
                ClearTitleMission(TitleMissionId.Level2320000);
            }
            if (e >= 2330000)
            {
                ClearTitleMission(TitleMissionId.Level2330000);
            }
            if (e >= 2340000)
            {
                ClearTitleMission(TitleMissionId.Level2340000);
            }
            if (e >= 2350000)
            {
                ClearTitleMission(TitleMissionId.Level2350000);
            }
            if (e >= 2360000)
            {
                ClearTitleMission(TitleMissionId.Level2360000);
            }
            if (e >= 2370000)
            {
                ClearTitleMission(TitleMissionId.Level2370000);
            }
            if (e >= 2380000)
            {
                ClearTitleMission(TitleMissionId.Level2380000);
            }
            if (e >= 2390000)
            {
                ClearTitleMission(TitleMissionId.Level2390000);
            }
            if (e >= 2400000)
            {
                ClearTitleMission(TitleMissionId.Level2400000);
            }
            if (e >= 2410000)
            {
                ClearTitleMission(TitleMissionId.Level2410000);
            }
            if (e >= 2420000)
            {
                ClearTitleMission(TitleMissionId.Level2420000);
            }
            if (e >= 2430000)
            {
                ClearTitleMission(TitleMissionId.Level2430000);
            }
            if (e >= 2440000)
            {
                ClearTitleMission(TitleMissionId.Level2440000);
            }
            if (e >= 2450000)
            {
                ClearTitleMission(TitleMissionId.Level2450000);
            }
            if (e >= 2460000)
            {
                ClearTitleMission(TitleMissionId.Level2460000);
            }
            if (e >= 2470000)
            {
                ClearTitleMission(TitleMissionId.Level2470000);
            }
            if (e >= 2480000)
            {
                ClearTitleMission(TitleMissionId.Level2480000);
            }
            if (e >= 2490000)
            {
                ClearTitleMission(TitleMissionId.Level2490000);
            }
            if (e >= 2500000)
            {
                ClearTitleMission(TitleMissionId.Level2500000);
            }
            if (e >= 2510000)
            {
                ClearTitleMission(TitleMissionId.Level2510000);
            }
            if (e >= 2520000)
            {
                ClearTitleMission(TitleMissionId.Level2520000);
            }
            if (e >= 2530000)
            {
                ClearTitleMission(TitleMissionId.Level2530000);
            }
            if (e >= 2540000)
            {
                ClearTitleMission(TitleMissionId.Level2540000);
            }
            if (e >= 2550000)
            {
                ClearTitleMission(TitleMissionId.Level2550000);
            }
            if (e >= 2560000)
            {
                ClearTitleMission(TitleMissionId.Level2560000);
            }
            if (e >= 2570000)
            {
                ClearTitleMission(TitleMissionId.Level2570000);
            }
            if (e >= 2580000)
            {
                ClearTitleMission(TitleMissionId.Level2580000);
            }
            if (e >= 2590000)
            {
                ClearTitleMission(TitleMissionId.Level2590000);
            }
            if (e >= 2600000)
            {
                ClearTitleMission(TitleMissionId.Level2600000);
            }
            if (e >= 2610000)
            {
                ClearTitleMission(TitleMissionId.Level2610000);
            }
            if (e >= 2620000)
            {
                ClearTitleMission(TitleMissionId.Level2620000);
            }
            if (e >= 2630000)
            {
                ClearTitleMission(TitleMissionId.Level2630000);
            }
            if (e >= 2640000)
            {
                ClearTitleMission(TitleMissionId.Level2640000);
            }
            if (e >= 2650000)
            {
                ClearTitleMission(TitleMissionId.Level2650000);
            }
            if (e >= 2660000)
            {
                ClearTitleMission(TitleMissionId.Level2660000);
            }
            if (e >= 2670000)
            {
                ClearTitleMission(TitleMissionId.Level2670000);
            }
            if (e >= 2680000)
            {
                ClearTitleMission(TitleMissionId.Level2680000);
            }
            if (e >= 2690000)
            {
                ClearTitleMission(TitleMissionId.Level2690000);
            }
            if (e >= 2700000)
            {
                ClearTitleMission(TitleMissionId.Level2700000);
            }
            if (e >= 2710000)
            {
                ClearTitleMission(TitleMissionId.Level2710000);
            }
            if (e >= 2720000)
            {
                ClearTitleMission(TitleMissionId.Level2720000);
            }
            if (e >= 2730000)
            {
                ClearTitleMission(TitleMissionId.Level2730000);
            }
            if (e >= 2740000)
            {
                ClearTitleMission(TitleMissionId.Level2740000);
            }
            if (e >= 2750000)
            {
                ClearTitleMission(TitleMissionId.Level2750000);
            }
            if (e >= 2760000)
            {
                ClearTitleMission(TitleMissionId.Level2760000);
            }
            if (e >= 2770000)
            {
                ClearTitleMission(TitleMissionId.Level2770000);
            }
            if (e >= 2780000)
            {
                ClearTitleMission(TitleMissionId.Level2780000);
            }
            if (e >= 2790000)
            {
                ClearTitleMission(TitleMissionId.Level2790000);
            }
            if (e >= 2800000)
            {
                ClearTitleMission(TitleMissionId.Level2800000);
            }
            if (e >= 2810000)
            {
                ClearTitleMission(TitleMissionId.Level2810000);
            }
            if (e >= 2820000)
            {
                ClearTitleMission(TitleMissionId.Level2820000);
            }
            if (e >= 2830000)
            {
                ClearTitleMission(TitleMissionId.Level2830000);
            }
            if (e >= 2840000)
            {
                ClearTitleMission(TitleMissionId.Level2840000);
            }
            if (e >= 2850000)
            {
                ClearTitleMission(TitleMissionId.Level2850000);
            }
            if (e >= 2860000)
            {
                ClearTitleMission(TitleMissionId.Level2860000);
            }
            if (e >= 2870000)
            {
                ClearTitleMission(TitleMissionId.Level2870000);
            }
            if (e >= 2880000)
            {
                ClearTitleMission(TitleMissionId.Level2880000);
            }
            if (e >= 2890000)
            {
                ClearTitleMission(TitleMissionId.Level2890000);
            }
            if (e >= 2900000)
            {
                ClearTitleMission(TitleMissionId.Level2900000);
            }
            if (e >= 2910000)
            {
                ClearTitleMission(TitleMissionId.Level2910000);
            }
            if (e >= 2920000)
            {
                ClearTitleMission(TitleMissionId.Level2920000);
            }
            if (e >= 2930000)
            {
                ClearTitleMission(TitleMissionId.Level2930000);
            }
            if (e >= 2940000)
            {
                ClearTitleMission(TitleMissionId.Level2940000);
            }
            if (e >= 2950000)
            {
                ClearTitleMission(TitleMissionId.Level2950000);
            }
            if (e >= 2960000)
            {
                ClearTitleMission(TitleMissionId.Level2960000);
            }
            if (e >= 2970000)
            {
                ClearTitleMission(TitleMissionId.Level2970000);
            }
            if (e >= 2980000)
            {
                ClearTitleMission(TitleMissionId.Level2980000);
            }
            if (e >= 2990000)
            {
                ClearTitleMission(TitleMissionId.Level2990000);
            }
            if (e >= 3000000)
            {
                ClearTitleMission(TitleMissionId.Level3000000);
            }
            if (e >= 3010000)
            {
                ClearTitleMission(TitleMissionId.Level3010000);
            }
            if (e >= 3020000)
            {
                ClearTitleMission(TitleMissionId.Level3020000);
            }
            if (e >= 3030000)
            {
                ClearTitleMission(TitleMissionId.Level3030000);
            }
            if (e >= 3040000)
            {
                ClearTitleMission(TitleMissionId.Level3040000);
            }
            if (e >= 3050000)
            {
                ClearTitleMission(TitleMissionId.Level3050000);
            }
            if (e >= 3060000)
            {
                ClearTitleMission(TitleMissionId.Level3060000);
            }
            if (e >= 3070000)
            {
                ClearTitleMission(TitleMissionId.Level3070000);
            }
            if (e >= 3080000)
            {
                ClearTitleMission(TitleMissionId.Level3080000);
            }
            if (e >= 3090000)
            {
                ClearTitleMission(TitleMissionId.Level3090000);
            }
            if (e >= 3100000)
            {
                ClearTitleMission(TitleMissionId.Level3100000);
            }
            if (e >= 3110000)
            {
                ClearTitleMission(TitleMissionId.Level3110000);
            }
            if (e >= 3120000)
            {
                ClearTitleMission(TitleMissionId.Level3120000);
            }
            if (e >= 3131000)
            {
                ClearTitleMission(TitleMissionId.Level3130000);
            }
            if (e >= 3140000)
            {
                ClearTitleMission(TitleMissionId.Level3140000);
            }
            if (e >= 3150000)
            {
                ClearTitleMission(TitleMissionId.Level3150000);
            }
            if (e >= 3160000)
            {
                ClearTitleMission(TitleMissionId.Level3160000);
            }
            if (e >= 3170000)
            {
                ClearTitleMission(TitleMissionId.Level3170000);
            }
            if (e >= 3180000)
            {
                ClearTitleMission(TitleMissionId.Level3180000);
            }
            if (e >= 3190000)
            {
                ClearTitleMission(TitleMissionId.Level3190000);
            }
            if (e >= 3200000)
            {
                ClearTitleMission(TitleMissionId.Level3200000);
            }


        }).AddTo(this);
        ServerData.userInfoTable.GetTableData(UserInfoTable.topClearStageId).AsObservable().Subscribe(e =>
        {
            if (e >= 100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage100);
            }
            if (e >= 150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage150);
            }
            if (e >= 200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage200);
            }
            if (e >= 250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage250);
            }
            if (e >= 300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage300);
            }
            if (e >= 350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage350);
            }
            if (e >= 400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage400);
            }
            if (e >= 450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage450);
            }
            if (e >= 500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage500);
            }
            if (e >= 550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage550);
            }
            if (e >= 600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage600);
            }
            if (e >= 650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage650);
            }
            if (e >= 700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage700);
            }
            if (e >= 750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage750);
            }
            if (e >= 800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage800);
            }
            if (e >= 850 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage850);
            }
            if (e >= 900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage900);
            }
            if (e >= 950 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage950);
            }
            if (e >= 1000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1000);
            }
            if (e >= 1050 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1050);
            }
            if (e >= 1100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1100);
            }
            if (e >= 1150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1150);
            }

            if (e >= 1200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1200);
            }

            if (e >= 1250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1250);
            }

            if (e >= 1300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1300);
            }

            if (e >= 1350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1350);
            }

            if (e >= 1400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1400);
            }

            if (e >= 1450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1450);
            }

            if (e >= 1500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1500);
            }
            //
            if (e >= 1550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1550);
            }

            if (e >= 1600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1600);
            }

            if (e >= 1650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1650);
            }

            if (e >= 1700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1700);
            }

            if (e >= 1750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1750);
            }

            if (e >= 1800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1800);
            }
            //
            if (e >= 1850 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1850);
            }
            if (e >= 1900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1900);
            }
            if (e >= 1950 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage1950);
            }
            if (e >= 2000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2000);
            }
            if (e >= 2050 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2050);
            }
            if (e >= 2100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2100);
            }
            if (e >= 2150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2150);
            }
            if (e >= 2200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2200);
            }

            if (e >= 2250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2250);
            }

            if (e >= 2300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2300);
            }

            if (e >= 2350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2350);
            }

            if (e >= 2400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2400);
            }
            //
            if (e >= 2450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2450);
            }

            if (e >= 2500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2500);
            }

            if (e >= 2550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2550);
            }

            if (e >= 2600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2600);
            }
            //
            if (e >= 2650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2650);
            }

            if (e >= 2700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2700);
            }

            if (e >= 2750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2750);
            }

            if (e >= 2800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2800);
            }
            //
            if (e >= 2850 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2850);
            }

            if (e >= 2900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2900);
            }

            if (e >= 2950 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage2950);
            }

            if (e >= 3000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3000);
            }
            //
            if (e >= 3050 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3050);
            }

            if (e >= 3100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3100);
            }

            if (e >= 3150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3150);
            }

            if (e >= 3200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3200);
            }

            if (e >= 3250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3250);
            }

            if (e >= 3300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3300);
            }

            if (e >= 3350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3350);
            }

            if (e >= 3400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3400);
            }

            if (e >= 3450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3450);
            }

            if (e >= 3500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3500);
            }

            if (e >= 3550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3550);
            }

            if (e >= 3600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3600);
            }
            //

            if (e >= 3650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3650);
            }
            if (e >= 3700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3700);
            }
            if (e >= 3750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3750);
            }
            if (e >= 3800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3800);
            }

            if (e >= 3850 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3850);
            }
            if (e >= 3900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3900);
            }
            if (e >= 3950 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage3950);
            }
            if (e >= 4000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4000);
            }

            //

            if (e >= 4050 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4050);
            }
            if (e >= 4100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4100);
            }
            if (e >= 4150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4150);
            }
            if (e >= 4200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4200);
            }

            if (e >= 4250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4250);
            }
            if (e >= 4300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4300);
            }
            if (e >= 4350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4350);
            }
            if (e >= 4400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4400);
            }

            //

            if (e >= 4450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4450);
            }
            if (e >= 4500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4500);
            }
            if (e >= 4550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4550);
            }
            if (e >= 4600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4600);
            }

            //

            if (e >= 4650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4650);
            }
            if (e >= 4700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4700);
            }
            if (e >= 4750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4750);
            }
            if (e >= 4800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4800);
            }

            //

            if (e >= 4850 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4850);
            }
            if (e >= 4900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4900);
            }
            if (e >= 4950 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage4950);
            }
            if (e >= 5000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5000);
            }

            //

            if (e >= 5050 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5050);
            }
            if (e >= 5100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5100);
            }
            if (e >= 5150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5150);
            }
            if (e >= 5200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5200);
            }

            if (e >= 5250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5250);
            }

            if (e >= 5300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5300);
            }

            if (e >= 5250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5250);
            }

            if (e >= 5300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5300);
            }
            //
            if (e >= 5350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5350);
            }

            if (e >= 5400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5400);
            }
            if (e >= 5450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5450);
            }

            if (e >= 5500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5500);
            }
            if (e >= 5550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5550);
            }

            if (e >= 5600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5600);
            }
            if (e >= 5650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5650);
            }

            if (e >= 5700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5700);
            }
            //
            if (e >= 5750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5750);
            }
            if (e >= 5800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5800);
            }
            if (e >= 5850 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5850);
            }
            if (e >= 5900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5900);
            }
            if (e >= 5950 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage5950);
            }
            if (e >= 6000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6000);
            }
            //
            if (e >= 6050 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6050);
            }
            if (e >= 6100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6100);
            }
            if (e >= 6150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6150);
            }
            if (e >= 6200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6200);
            }
            if (e >= 6250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6250);
            }
            if (e >= 6300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6300);
            }
            if (e >= 6350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6350);
            }
            if (e >= 6400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6400);
            }
            //
            if (e >= 6450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6450);
            }
            if (e >= 6500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6500);
            }
            if (e >= 6550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6550);
            }
            if (e >= 6600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6600);
            }

            //
            if (e >= 6650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6650);
            }
            if (e >= 6700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6700);
            }
            if (e >= 6750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6750);
            }
            if (e >= 6800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6800);
            }
            ////////////////
            ///
            if (e >= 6850 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6850);
            }
            if (e >= 6900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6900);
            }
            if (e >= 6950 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage6950);
            }
            if (e >= 7000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7000);
            }
            if (e >= 7050 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7050);
            }
            if (e >= 7100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7100);
            }
            if (e >= 7150 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7150);
            }
            if (e >= 7200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7200);
            }
            //

            if (e >= 7250 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7250);
            }
            if (e >= 7300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7300);
            }
            if (e >= 7350 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7350);
            }
            if (e >= 7400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7400);
            }
            if (e >= 7450 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7450);
            }
            if (e >= 7500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7500);
            }
            if (e >= 7550 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7550);
            }
            if (e >= 7600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7600);
            }
            //
            if (e >= 7650 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7650);
            }
            if (e >= 7700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7700);
            }
            if (e >= 7750 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7750);
            }
            if (e >= 7800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7800);
            }
            //
            if (e >= 7900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage7900);
            }
            if (e >= 8000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8000);
            }
            if (e >= 8100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8100);
            }
            if (e >= 8200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8200);
            }
            //
            if (e >= 8300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8300);
            }
            if (e >= 8400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8400);
            }
            if (e >= 8500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8500);
            }
            if (e >= 8600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8600);
            }
            //
            if (e >= 8700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8700);
            }
            if (e >= 8800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8800);
            }
            if (e >= 8900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage8900);
            }
            if (e >= 9000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9000);
            }
            //
            if (e >= 9100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9100);
            }
            if (e >= 9200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9200);
            }
            if (e >= 9300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9300);
            }
            if (e >= 9400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9400);
            }
            if (e >= 9500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9500);
            }
            if (e >= 9600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9600);
            }
            //
            if (e >= 9700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9700);
            }
            if (e >= 9800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9800);
            }
            //
            if (e >= 9900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage9900);
            }
            if (e >= 10000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10000);
            }
            /////
            ///        //
            if (e >= 10100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10100);
            }
            if (e >= 10200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10200);
            }
            //
            if (e >= 10300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10300);
            }
            if (e >= 10400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10400);
            }
            //
            if (e >= 10500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10500);
            }
            if (e >= 10600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10600);
            }
            //
            if (e >= 10700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10700);
            }
            if (e >= 10800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10800);
            }
            //
            if (e >= 10900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage10900);
            }
            if (e >= 11000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11000);
            }
            //
            if (e >= 11100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11100);
            }
            if (e >= 11200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11200);
            }
            //
            if (e >= 11300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11300);
            }
            if (e >= 11400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11400);
            }
            //
            if (e >= 11500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11500);
            }
            if (e >= 11600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11600);
            }
            //
            if (e >= 11700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11700);
            }
            if (e >= 11800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11800);
            }
            if (e >= 11900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage11900);
            }
            if (e >= 12000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12000);
            }
            //
            if (e >= 12100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12100);
            }
            if (e >= 12200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12200);
            }
            if (e >= 12300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12300);
            }
            if (e >= 12400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12400);
            }

            if (e >= 12500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12500);
            }

            if (e >= 12600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12600);
            }
            if (e >= 12700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12700);
            }

            if (e >= 12800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12800);
            }

            //

            if (e >= 12900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage12900);
            }

            if (e >= 13000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13000);
            }


            if (e >= 13100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13100);
            }

            if (e >= 13200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13200);
            }
            
            if (e >= 13300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13300);
            }
            
            if (e >= 13400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13400);
            }
            if (e >= 13500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13500);
            }
            if (e >= 13600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13600);
            }
            if (e >= 13700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13700);
            }
            if (e >= 13800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13800);
            }
            if (e >= 13900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage13900);
            }
            if (e >= 14000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14000);
            }
            if (e >= 14100 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14100);
            }
            if (e >= 14200 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14200);
            }
            if (e >= 14300 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14300);
            }
            if (e >= 14400 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14400);
            }
            if (e >= 14500 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14500);
            }
            if (e >= 14600 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14600);
            }
            if (e >= 14700 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14700);
            }
            if (e >= 14800 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14800);
            }
            if (e >= 14900 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage14900);
            }
            if (e >= 15000 - 1)
            {
                ClearTitleMission(TitleMissionId.Stage15000);
            }


        }).AddTo(this);

        ServerData.userInfoTable.GetTableData(UserInfoTable.marbleAwake).AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.AwakeMarble);
            }
        }).AddTo(this);

        //무기
        ServerData.weaponTable.TableDatas["weapon16"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendWeapon);
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas["weapon17"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendWeapon);
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas["weapon18"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendWeapon);
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas["weapon19"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendWeapon);
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas["weapon20"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetYomulWeapon);
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas["weapon21"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetYachaWeaopon);
            }
        }).AddTo(this);

        //필멸
        ServerData.weaponTable.TableDatas["weapon22"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetFeelMulGetEffect);
            }
        }).AddTo(this);

        //암
        ServerData.weaponTable.TableDatas["weapon23"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetFeelArm);
            }
        }).AddTo(this);
        //천
        ServerData.weaponTable.TableDatas["weapon24"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetFeelChun);
            }
        }).AddTo(this);
        //극
        ServerData.weaponTable.TableDatas["weapon25"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetFeelGuk);
            }
        }).AddTo(this);
        //인드라
        ServerData.weaponTable.TableDatas["weapon26"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetIndraWeapon);
            }
        }).AddTo(this);

        //나타
        ServerData.weaponTable.TableDatas["weapon27"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetNaTaWeapon);
            }
        }).AddTo(this);   
        ServerData.weaponTable.TableDatas["weapon28"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetOrochiWeapon);
            }
        }).AddTo(this);   

        ServerData.weaponTable.TableDatas["weapon29"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.FeelMulPaeWeapon);
            }
        }).AddTo(this);  
        
        ServerData.weaponTable.TableDatas["weapon30"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.FoxWeapon);
            }
        }).AddTo(this);   
        
        ServerData.weaponTable.TableDatas["weapon31"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.HellWeapon);
            }
        }).AddTo(this);
        //
        ServerData.weaponTable.TableDatas["weapon32"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.HellWeapon2);
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas["weapon33"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.YeoRaeWeapon);
            }
        }).AddTo(this);  

        ServerData.weaponTable.TableDatas["weapon34"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GangRimWeapon);
            }
        }).AddTo(this);      
        
        ServerData.weaponTable.TableDatas["weapon35"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.BulWeaponWeapon);
            }
        }).AddTo(this);    
        
        ServerData.weaponTable.TableDatas["weapon36"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.SainWeapon);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon43"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.ChunWeapon0);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon44"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.ChunWeapon1);
            }
        }).AddTo(this);


        ServerData.weaponTable.TableDatas["weapon50"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.ChunWeapon2);
            }
        }).AddTo(this);

        ServerData.weaponTable.TableDatas["weapon51"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.ChunWeapon3);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon57"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon0);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon58"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon1);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon59"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon2);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon63"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon3);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon64"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon4);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon65"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon5);
            }
        }).AddTo(this);
        
        ServerData.weaponTable.TableDatas["weapon66"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon6);
            }
        }).AddTo(this);
        ServerData.weaponTable.TableDatas["weapon77"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon7);
            }
        }).AddTo(this);
        ServerData.weaponTable.TableDatas["weapon78"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon8);
            }
        }).AddTo(this);
        ServerData.weaponTable.TableDatas["weapon79"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.DokebiWeapon9);
            }
        }).AddTo(this);
        ServerData.weaponTable.TableDatas["weapon80"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.SumisanWeapon0);
            }
        }).AddTo(this);
        ServerData.weaponTable.TableDatas["weapon84"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.SumisanWeapon1);
            }
        }).AddTo(this);
        ServerData.weaponTable.TableDatas["weapon85"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.SumisanWeapon2);
            }
        }).AddTo(this);
        ServerData.weaponTable.TableDatas["weapon86"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.SumisanWeapon3);
            }
        }).AddTo(this);

        //
        //노리개

        ServerData.magicBookTable.TableDatas["magicBook12"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendNorigae);
            }
        }).AddTo(this);

        ServerData.magicBookTable.TableDatas["magicBook13"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendNorigae);
            }
        }).AddTo(this);

        ServerData.magicBookTable.TableDatas["magicBook14"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendNorigae);
            }
        }).AddTo(this);

        ServerData.magicBookTable.TableDatas["magicBook15"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetLegendNorigae);
            }
        }).AddTo(this);

        ServerData.magicBookTable.TableDatas["magicBook16"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetSinMulNorigae);
            }
        }).AddTo(this);

        ServerData.magicBookTable.TableDatas["magicBook17"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetSinMulNorigae);
            }
        }).AddTo(this);

        ServerData.magicBookTable.TableDatas["magicBook18"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetSinMulNorigae);
            }
        }).AddTo(this);

        ServerData.magicBookTable.TableDatas["magicBook19"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.GetSinMulNorigae);
            }
        }).AddTo(this);

        //환수
        ServerData.petTable.TableDatas["pet4"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.EvolutionPet);
            }
        }).AddTo(this);

        ServerData.petTable.TableDatas["pet5"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.EvolutionPet);
            }
        }).AddTo(this);

        ServerData.petTable.TableDatas["pet6"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.EvolutionPet);
            }
        }).AddTo(this);

        ServerData.petTable.TableDatas["pet7"].hasItem.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.EvolutionPet);
            }
        }).AddTo(this);

        //영베
        ServerData.yomulServerTable.TableDatas["yomul0"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 100)
            {
                ClearTitleMission(TitleMissionId.Yomul0);
            }
        }).AddTo(this);

        //제물
        ServerData.yomulServerTable.TableDatas["yomul1"].hasAbil.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.Yomul1);
            }
        }).AddTo(this);

        //한계돌파
        ServerData.yomulServerTable.TableDatas["yomul2"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 1000)
            {
                ClearTitleMission(TitleMissionId.Yomul2);
            }
        }).AddTo(this);

        //심장베기
        ServerData.yomulServerTable.TableDatas["yomul3"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 1000)
            {
                ClearTitleMission(TitleMissionId.Yomul3);
            }
        }).AddTo(this);

        //시간베기
        ServerData.yomulServerTable.TableDatas["yomul4"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 100)
            {
                ClearTitleMission(TitleMissionId.Yomul4);
            }
        }).AddTo(this);

        //천공베기
        ServerData.yomulServerTable.TableDatas["yomul5"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 100)
            {
                ClearTitleMission(TitleMissionId.Yomul5);
            }
        }).AddTo(this);

        //약점베기
        ServerData.yomulServerTable.TableDatas["yomul6"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 100)
            {
                ClearTitleMission(TitleMissionId.Yomul6);
            }
        }).AddTo(this);

        //하늘베기
        ServerData.yomulServerTable.TableDatas["yomul7"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 1000)
            {
                ClearTitleMission(TitleMissionId.Yomul7);
            }
        }).AddTo(this);

        ServerData.yomulServerTable.TableDatas["yomul2"].level.AsObservable().Subscribe(e =>
        {
            if (e >= 3000)
            {
                ClearTitleMission(TitleMissionId.Yomul8);
            }
        }).AddTo(this);

        ServerData.petEquipmentServerTable.TableDatas["petequip0"].hasAbil.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.HyeonMu_1);
            }

        }).AddTo(this);

        ServerData.petEquipmentServerTable.TableDatas["petequip1"].hasAbil.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.BaekHo_1);
            }

        }).AddTo(this);

        ServerData.petEquipmentServerTable.TableDatas["petequip2"].hasAbil.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.ZuZak_1);
            }

        }).AddTo(this);

        ServerData.petEquipmentServerTable.TableDatas["petequip3"].hasAbil.AsObservable().Subscribe(e =>
        {
            if (e == 1)
            {
                ClearTitleMission(TitleMissionId.ChungRyong_1);
            }

        }).AddTo(this);
    }

    public void ClearTitleMission(TitleMissionId id)
    {
        return;
        var tableData = TableManager.Instance.TitleTable.dataArray[(int)id];
        var serverData = ServerData.titleServerTable.TableDatas[tableData.Stringid];

        //이미 클리어
        if (serverData.clearFlag.Value == 1)
        {
            return;
        }

        serverData.clearFlag.Value = 1;

        List<TransactionValue> transactions = new List<TransactionValue>();

        Param param = new Param();

        param.Add(tableData.Stringid, serverData.ConvertToString());

        transactions.Add(TransactionValue.SetUpdate(TitleServerTable.tableName, TitleServerTable.Indate, param));

        ServerData.SendTransaction(transactions);

        PlayerStats.ResetTitleHas();
    }
}
