using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.LoadingClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Linq;
using System.Reflection;
namespace BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses
{
    public static class AutoRegisterCommonInterface
    {
        public static void RegisterNonSavedClasses<V>(this IGamePackageRegister thisContainer)
        {
            thisContainer.RegisterSingleton<IPlayOrder, PlayOrderClass>();
            Assembly thisAssembly = Assembly.GetAssembly(typeof(V))!;
            CustomBasicList<Type> thisList = thisAssembly.GetTypes().Where(items => items.HasAttribute<SingletonGameAttribute>()).ToCustomBasicList();
            thisList.ForEach(items =>
            {
                thisContainer.RegisterSingleton(items);
            });

            thisList = thisAssembly.GetTypes().Where(items => items.HasAttribute<InstanceGameAttribute>()).ToCustomBasicList();
            thisList.ForEach(items =>
            {
                thisContainer.RegisterInstanceType(items);
            });

        }
        public static void RegisterCommonMiscCards<V, D>(this IGamePackageDIContainer thisContainer)
             where D : IDeckObject, new()
        {
            thisContainer.RegisterNonSavedClasses<V>();
            thisContainer.RegisterType<DeckObservablePile<D>>(true);
        }
        public static void RegisterCommonRegularCards<V, R>(this IGamePackageDIContainer thisContainer, bool aceLow = true, bool customDeck = false)
            where R : IRegularCard, new()
        {
            thisContainer.RegisterNonSavedClasses<V>();
            thisContainer.RegisterType<DeckObservablePile<R>>(true); //i think
            thisContainer.RegisterType<RegularCardsBasicShuffler<R>>(true); //if i needed a custom shuffler, rethink.
            bool rets = thisContainer.RegistrationExist<IRegularCardsSortCategory>();
            if (rets == true)
            {
                IRegularCardsSortCategory thisCat = thisContainer.Resolve<IRegularCardsSortCategory>();
                SortSimpleCards<R> thisSort = new SortSimpleCards<R>();
                thisSort.SuitForSorting = thisCat.SortCategory;
                thisContainer.RegisterSingleton(thisSort); //if we have a custom one, will already be picked up.
            }
            if (customDeck == false)
            {
                thisContainer.RegisterSingleton<IDeckCount, RegularAceHighSimpleDeck>();
                if (aceLow == true)
                {
                    thisContainer.RegisterSingleton<IRegularAceCalculator, RegularLowAceCalculator>(); //i think for now, if custom deck, then you have to manually register both.
                }
            }
        }
       
        public static void RegisterCommonMultplayerClasses<P, S>(this IGamePackageDIContainer container)
           where P : class, IPlayerItem, new()
           where S : BasicSavedGameClass<P>, new()
        {
            container.RegisterType<BasicGameLoader<P, S>>();
            container.RegisterType<RetrieveSavedPlayers<P, S>>();
            container.RegisterType<MultiplayerOpeningViewModel<P>>(); //obviously true
        }


        private static CustomBasicList<Type> ReplaceBoardGameColorClasses<E, P, S>()
            where E : struct, Enum
            where P : class, IPlayerBoardGame<E>, new()
            where S : BasicSavedGameClass<P>, new()
        {
            CustomBasicList<Type> output = new CustomBasicList<Type>()
            {
                typeof(BeginningColorProcessorClass<E, P, S>),
                typeof(BeginningColorModel<E, P>),
                typeof(BeginningChooseColorViewModel<E, P>)
            };
            return output;
        }

        public static void RegisterBeginningColors<E, P, S>(this IGamePackageDIContainer container, bool alsoStandardDice = true)
            where E : struct, Enum
            where P : class, IPlayerBoardGame<E>, new()
            where S : BasicSavedGameClass<P>, new()
        {
            container.RegisterType<BeginningColorProcessorClass<E, P, S>>();
            container.RegisterType<BeginningChooseColorViewModel<E, P>>();
            container.RegisterType<BeginningColorModel<E, P>>();
            if (alsoStandardDice)
            {
                container.RegisterType<StandardRollProcesses<SimpleDice, P>>();
                container.RegisterSingleton<IGenerateDice<int>, SimpleDice>();
            }
            MiscDelegates.GetMiscObjectsToReplace = ReplaceBoardGameColorClasses<E, P, S>;
        }

        //public static void RegisterBeginningColors<E, P, S>(this IGamePackageDIContainer container, bool a)
        //    where E : struct, Enum
        //    where P : class, IPlayerBoardGame<E>, new()
        //    where S : BasicSavedGameClass<P>, new()
        //{
        //    container.RegisterType<BeginningColorProcessorClass<E, P, S>>();
        //    container.RegisterType<BeginningChooseColorViewModel<E, P>>();
        //    container.RegisterType<BeginningColorModel<E, P>>();
        //    MiscDelegates.GetMiscObjectsToReplace = ReplaceBoardGameColorClasses<E, P, S>;
        //}

    }
}