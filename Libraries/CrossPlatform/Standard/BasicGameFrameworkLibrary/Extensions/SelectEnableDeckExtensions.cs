using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.GamePieceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace BasicGameFrameworkLibrary.Extensions
{
    public static class SelectEnableDeckExtensions
    {
        public static string GetColorFromEnum<E>(this BasicPickerData<E> piece) where E : struct, Enum
        {
            return piece.EnumValue.ToString().ToColor();
        }
        public static void SelectUnselectItem(this ISelectableObject thisItem)
        {
            thisItem.IsSelected = !thisItem.IsSelected;
        }
        public static void SelectUnselectItem<S>(this ISimpleList<S> thisList, int index) //has to be generics or casting problems.
            where S : ISelectableObject
        {
            int i = 0;
            foreach (var item in thisList)
            {
                if (i == index)
                {
                    item.SelectUnselectItem();
                    return;
                }
                i++;
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }
        public static void SelectSpecificItem<S, V>(this ISimpleList<S> thisList, Func<S, V> selector, V value)
            where S : ISelectableObject
        {
            thisList.ForEach(items =>
            {
                if (selector(items)!.Equals(value)) //has to be this way still.
                {
                    items.IsSelected = true;
                }
                else
                {
                    items.IsSelected = false;
                }
            });
        }
        public static void SelectSeveralItems<S, V>(this ISimpleList<S> thisList, Func<S, V> selector,
            ISimpleList<V> valuelist) where S : ISelectableObject
        {
            thisList.UnselectAllObjects(); //has to first select all objects.

            valuelist.ForEach(value =>
            {
                thisList.ForEach(items =>
                {
                    if (selector(items)!.Equals(value))
                    {
                        items.IsSelected = true;
                    }
                });
            });
        }
        public static CustomBasicList<S> GetSelectedItems<S>(this ISimpleList<S> thisList)
            where S : ISelectableObject => thisList.Where(items =>
            items.IsSelected == true).ToCustomBasicList();

        //this was still needed for games like blades of steele.
        public static DeckRegularDict<D> GetSelectedItems<D>(this IDeckDict<D> thisList)
           where D : IDeckObject => thisList.Where(items =>
           items.IsSelected == true).ToRegularDeckDict();

        public static int HowManySelectedItems<S>(this ISimpleList<S> thisList) where S : ISelectableObject
        {
            return thisList.Count(items => items.IsSelected);
        }

        public static void UnselectAllObjects<S>(this ISimpleList<S> thisList) where S : ISelectableObject
        {
            thisList.ForEach(items => items.IsSelected = false);
        }

        public static void SetEnabled<E>(this ISimpleList<E> thisList, bool isEnabled) where E : IEnabledObject
        {
            thisList.ForEach(items => items.IsEnabled = isEnabled);
        }
        public static async Task<CustomBasicList<int>> GetSavedIntegerListAsync(this string data)
        {
            return await js.DeserializeObjectAsync<CustomBasicList<int>>(data);
        }

        public static DeckRegularDict<D> ToRegularDeckDict<D>(this IEnumerable<D> thisList) where D : IDeckObject
        {
            return new DeckRegularDict<D>(thisList);
        }
        public static void SelectMaxOne<D>(this IDeckDict<D> thisList, D thisItem) where D : IDeckObject
        {
            if (thisItem.IsSelected == true)
            {
                thisItem.IsSelected = false;
                return;
            }
            thisList.ForEach(items => items.IsSelected = false);
            thisItem.IsSelected = true;
        }
        public static void UnhighlightObjects<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            thisList.ForEach(items =>
            {
                items.IsSelected = false;
                items.Drew = false;
            });
        }
        public static void RemoveSelectedItems<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            thisList.RemoveAllOnly(items => items.IsSelected == true);
        }
        public static void RemoveSelectedItems<D>(this IDeckDict<D> thisList, IDeckDict<D> itemsSelected) where D : IDeckObject
        {
            itemsSelected.ForEach(thisItem =>
            {
                thisList.RemoveObjectByDeck(thisItem.Deck); // i think.
            });
        }
        public static void SelectAllObjects<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            thisList.ForEach(items => items.IsSelected = true);
        }

        public static bool HasSelectedObject<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            return thisList.Any(items => items.IsSelected == true);
        }
        public static bool HasUnselectedObject<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            return thisList.Any(items => items.IsSelected == false);
        }
        public static void MakeAllObjectsVisible<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            thisList.ForEach(items => items.Visible = true);
        }
        public static void MakeAllObjectsKnown<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            thisList.ForEach(items => items.IsUnknown = false);
        }
        public static D GetLastObjectDrawn<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            return thisList.FindLast(items => items.Drew == true);
        }
        public static DeckRegularDict<D> GetObjectsFromList<D>(this CustomBasicList<int> thisList
            , IDeckDict<D> ListToRemove) where D : IDeckObject
        {
            DeckRegularDict<D> output = new DeckRegularDict<D>();
            thisList.ForEach(items =>
            {
                output.Add(ListToRemove.GetSpecificItem(items));
            });
            ListToRemove.RemoveGivenList(output, System.Collections.Specialized.NotifyCollectionChangedAction.Remove);
            return output;
        }
        public static async Task<DeckRegularDict<D>> GetObjectsFromDataAsync<D>(this string body
            , IDeckDict<D> ListToRemove) where D : IDeckObject
        {
            var temps = await js.DeserializeObjectAsync<CustomBasicList<int>>(body);
            return temps.GetObjectsFromList<D>(ListToRemove);
        }
        public static int ObjectsLeft(this IDeckDict<IDeckObject> thisList)
        {
            return thisList.Count(items => items.Visible == true);
        }
        public static int FindIndexByDeck<D>(this IDeckDict<D> thisList, int deck) where D : IDeckObject
        {
            D thisCard = thisList.GetSpecificItem(deck);
            return thisList.IndexOf(thisCard);
        }
        public static void ReplaceCardPlusRemove<D>(this DeckRegularDict<D> thisList, int oldDeck, int newDeck) where D : IDeckObject
        {
            int firstIndex = thisList.FindIndexByDeck(oldDeck);
            int secondIndex = thisList.FindIndexByDeck(newDeck);
            D thisCard = thisList.RemoveObjectByDeck(newDeck); //that one will disappear because its going somewhere else
            if (secondIndex > firstIndex)
            {
                thisList[firstIndex] = thisCard;
            }
            else
            {
                thisList[firstIndex - 1] = thisCard;
            }
            thisList.ReplaceDictionary(oldDeck, newDeck, thisCard);
        }
        public static D GetLastObject<D>(this IDeckDict<D> thisList, bool alsoRemove) where D : IDeckObject
        {
            D output = thisList.Last();
            if (alsoRemove == true)
            {
                thisList.RemoveSpecificItem(output);
            }
            return output;
        }
        public static D GetFirstObject<D>(this IDeckDict<D> thisList, bool alsoRemove) where D : IDeckObject
        {
            D output = thisList.First();
            if (alsoRemove == true)
            {
                thisList.RemoveSpecificItem(output);
            }
            return output;
        }
        public static CustomBasicList<int> GetDeckListFromObjectList<D>(this IDeckDict<D> thisList) where D : IDeckObject
        {
            return thisList.Select(Items => Items.Deck).ToCustomBasicList();
        }
        public static DeckRegularDict<D> GetNewObjectListFromDeckList<D>(this CustomBasicList<int> thisList,
            IDeckLookUp<D> ThisBase) where D : IDeckObject
        {
            DeckRegularDict<D> output = new DeckRegularDict<D>();
            thisList.ForEach(items =>
            {
                output.Add(ThisBase.GetSpecificItem(items));
            });
            return output;
        }
        public static async Task<DeckRegularDict<D>> GetNewObjectListFromDeckListAsync<D>(this string data,
            IDeckLookUp<D> thisBase) where D : IDeckObject
        {
            DeckRegularDict<D> output = new DeckRegularDict<D>();

            CustomBasicList<int> thisList = await js.DeserializeObjectAsync<CustomBasicList<int>>(data);
            thisList.ForEach(items =>
            {
                output.Add(thisBase.GetSpecificItem(items));
            });
            return output;
        }
        public static int TotalPoints<P>(this IDeckDict<P> thisList)
            where P : IDeckObject, IPointsObject
        {
            return thisList.Sum(items => items.GetPoints);
        }
        public static DeckRegularDict<D> CardsFromAllPlayers<D, P>(this PlayerCollection<P> playerList)
            where D : IDeckObject, new()
            where P : class, IPlayerSingleHand<D>, new()
        {
            DeckRegularDict<D> output = new DeckRegularDict<D>();
            playerList.ForEach(thisPlayer => output.AddRange(thisPlayer.MainHandList));
            return output;
        }
        public static int WhoHasCardFromDeck<D, P>(this PlayerCollection<P> playerList, int deck)
            where D : IDeckObject, new()
            where P : class, IPlayerSingleHand<D>, new()
        {
            foreach (var thisPlayer in playerList)
            {
                if (thisPlayer.MainHandList.ObjectExist(deck))
                {
                    return thisPlayer.Id;
                }
            }
            throw new BasicBlankException($"Nobody had deck of {deck}");
        }

        public static EnumSuitList GetRegularSuit<E>(this E ThisValue)
            where E : Enum
        {
            return (EnumSuitList)Enum.Parse(typeof(EnumSuitList), ThisValue.ToString());
        }
    }
}