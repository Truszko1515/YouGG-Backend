using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Enums
{
    public enum SpellImagesEnum
    {
        [Description("SummonerBarrier.png")]
        SummonerBarrier = 21,

        [Description("SummonerBoost.png")]
        SummonerBoost = 1,

        [Description("SummonerCherryFlash.png")]
        SummonerCherryFlash = 2202,

        [Description("SummonerCherryHold.png")]
        SummonerCherryHold = 2201,

        [Description("SummonerDot.png")]
        SummonerDot = 14,

        [Description("SummonerExhaust.png")]
        SummonerExhaust = 3,

        [Description("SummonerFlash.png")]
        SummonerFlash = 4,

        [Description("SummonerHaste.png")]
        SummonerHaste = 6,

        [Description("SummonerHeal.png")]
        SummonerHeal = 7,

        [Description("SummonerMana.png")]
        SummonerMana = 13,

        [Description("SummonerPoroRecall.png")]
        SummonerPoroRecall = 30,

        [Description("SummonerPoroThrow.png")]
        SummonerPoroThrow = 31,

        [Description("SummonerSmite.png")]
        SummonerSmite = 11,

        [Description("SummonerSnowURFSnowball_Mark.png")]
        SummonerSnowURFSnowball_Mark = 39,

        [Description("SummonerSnowball.png")]
        SummonerSnowball = 32,

        [Description("SummonerTeleport.png")]
        SummonerTeleport = 12,

        [Description("Summoner_UltBookPlaceholder.png")]
        Summoner_UltBookPlaceholder = 54,

        [Description("Summoner_UltBookSmitePlaceholder.png")]
        Summoner_UltBookSmitePlaceholder = 55
    }
    public enum RuneIcons
    {
        [Description("perk-images/Styles/7200_Domination.png")]
        Domination = 8100,

        [Description("perk-images/Styles/Domination/Electrocute/Electrocute.png")]
        Electrocute = 8112,

        [Description("perk-images/Styles/Domination/DarkHarvest/DarkHarvest.png")]
        DarkHarvest = 8128,

        [Description("perk-images/Styles/Domination/HailOfBlades/HailOfBlades.png")]
        HailOfBlades = 9923,

        [Description("perk-images/Styles/Domination/CheapShot/CheapShot.png")]
        CheapShot = 8126,

        [Description("perk-images/Styles/Domination/TasteOfBlood/GreenTerror_TasteOfBlood.png")]
        TasteOfBlood = 8139,

        [Description("perk-images/Styles/Domination/SuddenImpact/SuddenImpact.png")]
        SuddenImpact = 8143,

        [Description("perk-images/Styles/Domination/ZombieWard/ZombieWard.png")]
        ZombieWard = 8136,

        [Description("perk-images/Styles/Domination/GhostPoro/GhostPoro.png")]
        GhostPoro = 8120,

        [Description("perk-images/Styles/Domination/EyeballCollection/EyeballCollection.png")]
        EyeballCollection = 8138,

        [Description("perk-images/Styles/Domination/TreasureHunter/TreasureHunter.png")]
        TreasureHunter = 8135,

        [Description("perk-images/Styles/Domination/RelentlessHunter/RelentlessHunter.png")]
        RelentlessHunter = 8105,

        [Description("perk-images/Styles/Domination/UltimateHunter/UltimateHunter.png")]
        UltimateHunter = 8106,

        [Description("perk-images/Styles/7203_Whimsy.png")]
        Inspiration = 8300,

        [Description("perk-images/Styles/Inspiration/GlacialAugment/GlacialAugment.png")]
        GlacialAugment = 8351,

        [Description("perk-images/Styles/Inspiration/UnsealedSpellbook/UnsealedSpellbook.png")]
        UnsealedSpellbook = 8360,

        [Description("perk-images/Styles/Inspiration/FirstStrike/FirstStrike.png")]
        FirstStrike = 8369,

        [Description("perk-images/Styles/Inspiration/HextechFlashtraption/HextechFlashtraption.png")]
        HextechFlashtraption = 8306,

        [Description("perk-images/Styles/Inspiration/MagicalFootwear/MagicalFootwear.png")]
        MagicalFootwear = 8304,

        [Description("perk-images/Styles/Inspiration/CashBack/CashBack2.png")]
        CashBack = 8321,

        [Description("perk-images/Styles/Inspiration/PerfectTiming/AlchemistCabinet.png")]
        PerfectTiming = 8313,

        [Description("perk-images/Styles/Inspiration/TimeWarpTonic/TimeWarpTonic.png")]
        TimeWarpTonic = 8352,

        [Description("perk-images/Styles/Inspiration/BiscuitDelivery/BiscuitDelivery.png")]
        BiscuitDelivery = 8345,

        [Description("perk-images/Styles/Inspiration/CosmicInsight/CosmicInsight.png")]
        CosmicInsight = 8347,

        [Description("perk-images/Styles/Resolve/ApproachVelocity/ApproachVelocity.png")]
        ApproachVelocity = 8410,

        [Description("perk-images/Styles/Inspiration/JackOfAllTrades/JackofAllTrades2.png")]
        JackOfAllTrades = 8316,

        [Description("perk-images/Styles/7201_Precision.png")]
        Precision = 8000,

        [Description("perk-images/Styles/Precision/PressTheAttack/PressTheAttack.png")]
        PressTheAttack = 8005,

        [Description("perk-images/Styles/Precision/FleetFootwork/FleetFootwork.png")]
        FleetFootwork = 8021,

        [Description("perk-images/Styles/Precision/Conqueror/Conqueror.png")]
        Conqueror = 8010,

        [Description("perk-images/Styles/Precision/AbsorbLife/AbsorbLife.png")]
        AbsorbLife = 9101,

        [Description("perk-images/Styles/Precision/Triumph.png")]
        Triumph = 9111,

        [Description("perk-images/Styles/Precision/PresenceOfMind/PresenceOfMind.png")]
        PresenceOfMind = 8009,

        [Description("perk-images/Styles/Precision/LegendAlacrity/LegendAlacrity.png")]
        LegendAlacrity = 9104,

        [Description("perk-images/Styles/Precision/LegendHaste/LegendHaste.png")]
        LegendTenacity = 9105,

        [Description("perk-images/Styles/Precision/LegendBloodline/LegendBloodline.png")]
        LegendBloodline = 9103,

        [Description("perk-images/Styles/Precision/CoupDeGrace/CoupDeGrace.png")]
        CoupDeGrace = 8014,

        [Description("perk-images/Styles/Precision/CutDown/CutDown.png")]
        CutDown = 8017,

        [Description("perk-images/Styles/Sorcery/LastStand/LastStand.png")]
        LastStand = 8299,

        [Description("perk-images/Styles/7204_Resolve.png")]
        Resolve = 8400,

        [Description("perk-images/Styles/Resolve/GraspOfTheUndying/GraspOfTheUndying.png")]
        GraspOfTheUndying = 8437,

        [Description("perk-images/Styles/Resolve/VeteranAftershock/VeteranAftershock.png")]
        Aftershock = 8439,

        [Description("perk-images/Styles/Resolve/Guardian/Guardian.png")]
        Guardian = 8465,

        [Description("perk-images/Styles/Resolve/Demolish/Demolish.png")]
        Demolish = 8446,

        [Description("perk-images/Styles/Resolve/FontOfLife/FontOfLife.png")]
        FontOfLife = 8463,

        [Description("perk-images/Styles/Resolve/MirrorShell/MirrorShell.png")]
        ShieldBash = 8401,

        [Description("perk-images/Styles/Resolve/Conditioning/Conditioning.png")]
        Conditioning = 8429,

        [Description("perk-images/Styles/Resolve/SecondWind/SecondWind.png")]
        SecondWind = 8444,

        [Description("perk-images/Styles/Resolve/BonePlating/BonePlating.png")]
        BonePlating = 8473,

        [Description("perk-images/Styles/Resolve/Overgrowth/Overgrowth.png")]
        Overgrowth = 8451,

        [Description("perk-images/Styles/Resolve/Revitalize/Revitalize.png")]
        Revitalize = 8453,

        [Description("perk-images/Styles/Sorcery/Unflinching/Unflinching.png")]
        Unflinching = 8242,

        [Description("perk-images/Styles/7202_Sorcery.png")]
        Sorcery = 8200,

        [Description("perk-images/Styles/Sorcery/SummonAery/SummonAery.png")]
        SummonAery = 8214,

        [Description("perk-images/Styles/Sorcery/ArcaneComet/ArcaneComet.png")]
        ArcaneComet = 8229,

        [Description("perk-images/Styles/Sorcery/PhaseRush/PhaseRush.png")]
        PhaseRush = 8230,

        [Description("perk-images/Styles/Sorcery/NullifyingOrb/Pokeshield.png")]
        NullifyingOrb = 8224,

        [Description("perk-images/Styles/Sorcery/ManaflowBand/ManaflowBand.png")]
        ManaflowBand = 8226,

        [Description("perk-images/Styles/Sorcery/NimbusCloak/6361.png")]
        NimbusCloak = 8275,

        [Description("perk-images/Styles/Sorcery/Transcendence/Transcendence.png")]
        Transcendence = 8210,

        [Description("perk-images/Styles/Sorcery/Celerity/CelerityTemp.png")]
        Celerity = 8234,

        [Description("perk-images/Styles/Sorcery/AbsoluteFocus/AbsoluteFocus.png")]
        AbsoluteFocus = 8233,

        [Description("perk-images/Styles/Sorcery/Scorch/Scorch.png")]
        Scorch = 8237,

        [Description("perk-images/Styles/Sorcery/Waterwalking/Waterwalking.png")]
        Waterwalking = 8232,

        [Description("perk-images/Styles/Sorcery/GatheringStorm/GatheringStorm.png")]
        GatheringStorm = 8236
    }

}
