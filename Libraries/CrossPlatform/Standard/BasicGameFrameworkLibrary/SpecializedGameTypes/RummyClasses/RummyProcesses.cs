using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses
{
    public class RummyProcesses<S, C, R>
        where S : Enum
        where C : Enum
        where R : IRummmyObject<S, C>, new() //i think this is needed too.
    {
        public bool HasSecond { get; set; }
        struct TempObject //not necessary a card
        {
            public int ObjectNumber;
            public S Suit; //i think.
            public int IndexinCollection;
        }
        public bool NeedMatch { get; set; } = true; // most of the times; needs a match
        public bool HasWild { get; set; }
        private int _maxStraight;
        public bool UseAll { get; set; }
        public int LowNumber { get; set; } = 1;
        public int HighNumber { get; set; } = 13; // from 1 to 13 because the regular deck of cards
        private CustomBasicList<R>? _tempList; //can't use dictionary one because it may be used for tiles too like rummy tiles which has no deck
        public bool UseSecond { get; private set; }
        public int FirstUsed { get; private set; }
        private void CheckErrors()
        {
            if (HighNumber == 0 | LowNumber == 0)
            {
                throw new Exception("Need to know the low and high numbers");
            }
            if (HighNumber < LowNumber)
            {
                throw new Exception("The highest number must be higher than the low number");
            }
            _maxStraight = HighNumber - LowNumber + 2;
        }
        private void GetTempList(ICustomBasicList<R> objectList)
        {
            _tempList = objectList.ToCustomBasicList();
        }
        public CustomBasicList<S> ListSuits(ICustomBasicList<R> objectList)
        {
            GetTempList(objectList);
            var thisTemp = _tempList.Where(Items => Items.IsObjectIgnored == false && Items.IsObjectWild == false).GroupBy(Items => Items.GetSuit);
            CustomBasicList<S> output = new CustomBasicList<S>();
            int MaxNum = 0;
            int Count;
            foreach (var newTemp in thisTemp)
            {
                Count = newTemp.Count();
                if (Count > MaxNum)
                {
                    output = new CustomBasicList<S>();
                    MaxNum = Count;
                }
                output.Add(newTemp.Key);
            }
            return output;
        }
        public int StraightDistance(ref ICustomBasicList<R> objectList, int whatNum)
        {
            int output = default;
            GetTempList(objectList);
            int highAmount = default;
            int lowAmount = default;
            int firstInfo = default;
            int secondInfo = default;
            if (NeedMatch == true)
            {
                _tempList = _tempList.OrderBy(Items => Items.GetSuit).ThenBy(Items => Items.ReadMainValue).ToCustomBasicList();
            }
            else
            {
                _tempList = _tempList.OrderBy(Items => Items.ReadMainValue).ToCustomBasicList(); //this was the best way to handle sorting.
            }
            int x = 0;
            int newNum = 0;
            do //0 based
            {
                if (x > _tempList.Count)
                    break;
                newNum = _tempList[x].ReadMainValue;
                if (newNum == whatNum)
                {
                    if (x > 0)
                    {
                        lowAmount = _tempList[x].ReadMainValue;
                        firstInfo = lowAmount;
                        lowAmount = whatNum - lowAmount;
                    }
                    else
                    {
                        lowAmount = 0;
                        firstInfo = 0;
                    }
                    if (x + 1 < objectList.Count)
                    {
                        highAmount = _tempList[x + 1].ReadMainValue;
                        secondInfo = highAmount;
                        if (highAmount > 15)
                        {
                            highAmount = 0;
                            secondInfo = 0;
                        }
                        else
                        {
                            highAmount -= whatNum;
                        }
                    }
                    else
                    {
                        highAmount = 0;
                    }
                    break;
                }
                x += 1;
            }
            while (true);
            if (highAmount == 0)
            {
                output = lowAmount;
                FirstUsed = firstInfo;
            }
            else if (lowAmount == 0)
            {
                output = highAmount;
                FirstUsed = secondInfo;
            }
            else if (highAmount < lowAmount)
            {
                output = highAmount;
                FirstUsed = secondInfo;
            }
            else
            {
                output = lowAmount;
                FirstUsed = secondInfo;
            }
            if (HasSecond == false)
            {
                return output;
            }
            GetTempList(objectList);
            if (NeedMatch == true)
            {
                _tempList = _tempList.OrderBy(Items => Items.GetSuit).ThenBy(Items => Items.GetSecondNumber).ToCustomBasicList();
            }
            else
            {
                _tempList = _tempList.OrderBy(Items => Items.GetSecondNumber).ToCustomBasicList(); //this was the best way to handle sorting.
            }
            x = 0;
            do //0 based
            {
                if (x > _tempList.Count)
                {
                    break;
                }
                newNum = _tempList[x].GetSecondNumber;
                if (newNum == whatNum)
                {
                    if (x > 0)
                    {
                        lowAmount = _tempList[x].GetSecondNumber;
                        firstInfo = lowAmount;
                        lowAmount = whatNum - lowAmount;
                    }
                    else
                    {
                        lowAmount = 0;
                        firstInfo = 0;
                    }

                    if (x + 1 < objectList.Count)
                    {
                        highAmount = _tempList[x + 1].GetSecondNumber;
                        secondInfo = highAmount;
                        if (highAmount > 15)
                        {
                            highAmount = 0;
                            secondInfo = 0;
                        }
                        else
                        {
                            highAmount -= whatNum;
                        }
                    }
                    else
                    {
                        highAmount = 0;
                    }
                    break;
                }
                x += 1;
            }
            while (true);
            if (output == 0 & highAmount == 0 & lowAmount > 0)
            {
                output = lowAmount;
                FirstUsed = firstInfo;
            }
            else if (output == 0 & lowAmount == 0 & highAmount > 0)
            {
                output = highAmount;
                FirstUsed = secondInfo;
            }
            else if (highAmount == 0 & lowAmount < output & lowAmount > 0)
            {
                output = lowAmount;
                FirstUsed = firstInfo;
            }
            else if (lowAmount == 0 & highAmount < output & highAmount > 0)
            {
                output = highAmount;
                FirstUsed = secondInfo;
            }
            else if (highAmount < lowAmount & highAmount < output & highAmount > 0)
            {
                output = highAmount;
                FirstUsed = secondInfo;
            }
            else if (lowAmount < highAmount & lowAmount < output & highAmount > 0)
            {
                output = lowAmount;
                FirstUsed = firstInfo;
            }
            return output;
        }
        private void HighLow(ref CustomBasicList<R> firstList, ICustomBasicList<R> colObj, int intHowMany, bool minonly, int lngUnUsedWild, ref int intHigh, ref int intLow)
        {
            int intObjectNumber;
            int intAvailableObject;
            int intAvailableWild;
            intHigh = 0;
            intLow = 0;
            if (colObj.Count <= 0)
            {
                return;
            }
            if (HasWild)
            {
                intAvailableWild = lngUnUsedWild;
            }
            else
            {
                intAvailableWild = 0;
            }
            intAvailableObject = colObj.Count;
            if (UseSecond == false)
                firstList = (from Items in colObj
                             orderby Items.ReadMainValue
                             select Items).ToCustomBasicList();
            else
                firstList = (from Items in colObj
                             orderby Items.GetSecondNumber
                             select Items).ToCustomBasicList();
            if (UseSecond == false)
            {
                intObjectNumber = firstList.Last().ReadMainValue;
            }
            else
            {
                intObjectNumber = firstList.Last().GetSecondNumber;
            }
            if (intObjectNumber < HighNumber)
            {
                if (intAvailableWild > 0)
                {
                    if (minonly)
                    {
                        if (intHowMany > intAvailableObject)
                            if (intHowMany - intAvailableObject > HighNumber - intObjectNumber)
                            {
                                intHigh = HighNumber;
                                intAvailableWild -= HighNumber - intObjectNumber;
                                intAvailableObject += HighNumber - intObjectNumber;
                            }
                            else
                            {
                                intHigh = intObjectNumber + intHowMany - intAvailableObject;
                                intAvailableWild -= intHowMany - intAvailableObject;
                                intAvailableObject = intHowMany;
                            }
                        else if (HighNumber - intObjectNumber > intAvailableWild)
                        {
                            intHigh = intObjectNumber + intAvailableWild;
                            intAvailableWild = 0;
                        }
                        else
                        {
                            intHigh = HighNumber;
                            intAvailableWild -= HighNumber - intObjectNumber;
                        }
                    }
                    else
                    {
                        intHigh = intObjectNumber;
                    }
                }
            }
            if (UseSecond == false)
            {
                intObjectNumber = colObj.First().ReadMainValue;
            }
            else
            {
                intObjectNumber = colObj.First().GetSecondNumber;
            }
            if (intObjectNumber >= LowNumber)
            {
                if (intAvailableWild > 0)
                {
                    if (minonly)
                    {
                        if (intHowMany > intAvailableObject)
                        {
                            if (intHowMany - intAvailableObject > intObjectNumber - LowNumber)
                            {
                                intLow = LowNumber;
                                intAvailableWild -= intObjectNumber - LowNumber;
                                intAvailableObject += intObjectNumber - LowNumber;
                            }
                            else
                            {
                                intLow = intObjectNumber - (intHowMany - intAvailableObject);
                                intAvailableWild -= intHowMany - intAvailableObject;
                                intAvailableObject = intHowMany;
                            }
                        }
                        else if (intObjectNumber - LowNumber > intAvailableWild)
                        {
                            intLow = intObjectNumber - intAvailableWild;
                            intAvailableWild = 0;
                        }
                        else
                        {
                            intLow = LowNumber;
                            intAvailableWild -= intObjectNumber - LowNumber;
                        }
                    }
                    else
                    {
                        intLow = intObjectNumber;
                    }
                }
            }   
        }
        private bool HasValidStraight(ICustomBasicList<R> firstList, ICustomBasicList<R> wildList, bool bUseSecond, int lngHowMany, bool minonly, ref int[]? aStraightObject, ref int lngUnUsedWild, ref int startAt)
        {
            CustomBasicList<TempObject> aObject = new CustomBasicList<TempObject>();
            TempObject tempObject;
            int lngObjectIndex = default;
            int lngIndex;
            int lngAvailableWild;
            int lngTotalWild;
            int lngObjectInStraight;
            int lngEnd;
            bool output = true; //this time it was set to true.
            int intObjectNumber;
            if (firstList.Count + wildList.Count < lngHowMany)
            {
                return false;
            }
            if (lngHowMany == 1)
            {
                return true;
            }
            if (HasWild)
            {
                lngTotalWild = wildList.Count(); // i think
                lngAvailableWild = lngTotalWild; // 'initally available wild will be same as total wild
                lngUnUsedWild = lngTotalWild;
            }
            else
            {
                lngTotalWild = 0;
                lngAvailableWild = 0;
                lngUnUsedWild = 0;
            }
            S CurrentSuit;
            var loopTo = firstList.Count - 1;
            for (lngIndex = 0; lngIndex <= loopTo; lngIndex++)
            {
                tempObject = new TempObject();
                if (NeedMatch == true)
                {
                    CurrentSuit = firstList[lngIndex].GetSuit;
                    tempObject.Suit = CurrentSuit;
                }
                intObjectNumber = firstList[lngIndex].ReadMainValue;
                if (bUseSecond)
                {
                    intObjectNumber = firstList[lngIndex].GetSecondNumber;
                }
                tempObject.ObjectNumber = intObjectNumber;
                tempObject.IndexinCollection = lngIndex;
                aObject.Add(tempObject);
                lngObjectIndex++; //i think
            }

            if (output == false)
            {
                return false;
            }
            if (lngObjectIndex + lngTotalWild < lngHowMany)
            {
                return false;
            }
            lngObjectInStraight = 1;
            startAt = 0;
            lngEnd = 0;
            var loopTo1 = lngObjectIndex - (long)2;
            for (lngIndex = 0; lngIndex <= loopTo1; lngIndex++)
            {
                if (Math.Abs(aObject[lngIndex].ObjectNumber - aObject[lngIndex + 1].ObjectNumber) == 1 & (NeedMatch == false | NeedMatch == true & aObject[lngIndex].Suit.Equals(aObject[lngIndex + 1].Suit)))
                {
                    lngObjectInStraight += 1;
                    lngEnd = lngIndex + 1;
                }
                else if (HasWild == true)
                {
                    if ((long)(Math.Abs(aObject[lngIndex].ObjectNumber - aObject[lngIndex + 1].ObjectNumber) - 1) <= lngAvailableWild & lngAvailableWild > 0)
                        if (minonly == true)
                        {
                            if (lngObjectInStraight < lngHowMany)
                            {
                                lngAvailableWild -= lngHowMany - lngObjectInStraight;
                                lngObjectInStraight = lngObjectInStraight + lngHowMany - lngObjectInStraight;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            lngObjectInStraight++;
                            lngObjectInStraight = lngObjectInStraight + Math.Abs(aObject[lngIndex].ObjectNumber - aObject[lngIndex + 1].ObjectNumber) - 1;
                            lngAvailableWild -= Math.Abs(aObject[lngIndex].ObjectNumber - aObject[lngIndex + 1].ObjectNumber) - 1;
                            lngEnd = lngIndex + 1;
                        }
                    else if (lngObjectInStraight >= lngHowMany)
                    {
                        lngEnd = lngIndex;
                        break;
                    }
                    else
                    {
                        if (lngObjectIndex - lngIndex - (long)1 + lngTotalWild >= lngHowMany)
                        {
                            startAt = lngIndex + 1;
                            lngAvailableWild = lngTotalWild;
                            lngObjectInStraight = 1;
                            lngEnd = startAt;
                        }
                        else
                        {
                            break;
                        }
                    }
                        
                }
                   
                else if (lngObjectInStraight >= lngHowMany)
                {
                    lngEnd = lngIndex;
                    break;
                }
                else
                {
                    startAt = lngIndex + 1;
                    lngAvailableWild = lngTotalWild; // 'Make all wild available
                    lngObjectInStraight = 1;
                    lngEnd = startAt;
                }
                if (lngObjectInStraight >= lngHowMany & minonly == true)
                {
                    break;
                }
            }
            if (lngObjectInStraight + lngAvailableWild >= lngHowMany)
            {
                aStraightObject = new int[lngObjectInStraight - (lngTotalWild - lngAvailableWild) + 1];
                long lngPos;
                lngPos = 0;
                var loopTo2 = lngEnd;
                for (lngIndex = startAt; lngIndex <= loopTo2; lngIndex++)
                {
                    aStraightObject[lngPos] = aObject[lngIndex].IndexinCollection;
                    lngPos++;
                }
                output = true;
                lngUnUsedWild = lngAvailableWild;
            }
            else
            {
                output = false;
            }
            return output;
        }
        private ICustomBasicList<R> StraightSet(ICustomBasicList<R> objectList, int howMany, bool minOnly, ICustomBasicList<R> wildList, bool noWilds = false)
        {
            ICustomBasicList<R> output = new CustomBasicList<R>();
            bool tempwilds = HasWild;
            if (noWilds == true)
            {
                HasWild = false;
            }
            else if (HasWild == false)
            {
                HasWild = false;
            }
            else
            {
                HasWild = true;
            }
            IEnumerable<R> firstLinq; //we may need that unfortunately.
            if (UseSecond == false)
                firstLinq = from Objects in _tempList
                            where Objects.IsObjectIgnored == false && Objects.IsObjectWild == false
                            orderby Objects.ReadMainValue
                            select Objects;
            else
                firstLinq = from Objects in _tempList
                            where Objects.IsObjectIgnored == false && Objects.IsObjectWild == false
                            select Objects;
            CustomBasicList<R> firstList = new CustomBasicList<R>();
            firstList.AddRange(firstLinq);
            var exps = firstLinq.GroupBy(Items => Items.ReadMainValue).ToCustomBasicList();
            CustomBasicList<R> temps;
            if (NeedMatch == false)
            {
                temps = firstList.GroupBy(Items => Items.ReadMainValue).Select(Items => Items.First()).ToCustomBasicList();
            }
            else
            {
                temps = firstList.GroupBy(Items => new { Items.ReadMainValue, Items.GetSuit }).Select(Items => Items.First()).ToCustomBasicList();
            }
            firstList.ReplaceRange(temps);
            if (NeedMatch == true)
            {
                firstList = firstList.OrderBy(Items => Items.GetSuit).ThenBy(Items => Items.ReadMainValue).ToCustomBasicList();
            }
            else
            {
                firstList = firstList.OrderBy(Items => Items.ReadMainValue).ToCustomBasicList();
            }
            int[]? aObjectIndex;
            aObjectIndex = new int[1];
            bool bStraightFound = default;
            int lngUnUsedWild = default;
            int Start = default;
            if (HasValidStraight(firstList, wildList, false, howMany, minOnly, ref aObjectIndex, ref lngUnUsedWild, ref Start))
            {
                bStraightFound = true;
            }
            else
            {
                aObjectIndex = null;
                if (HasSecond == true)
                {
                    firstList = new CustomBasicList<R>();
                    firstList.AddRange(firstLinq);
                    if (NeedMatch == true)
                    {
                        firstList = (from Items in firstList
                                     orderby Items.GetSuit ascending, Items.GetSecondNumber ascending
                                     select Items).ToCustomBasicList();
                    }
                    else
                    {
                        firstList = (from Items in firstList
                                     orderby Items.GetSecondNumber
                                     select Items).ToCustomBasicList();
                    }
                }
                if (HasValidStraight(firstList, wildList, true, howMany, minOnly, ref aObjectIndex, ref lngUnUsedWild, ref Start))
                {
                    UseSecond = true;
                    bStraightFound = true;
                }
            }
            int lngObjectInStraight;
            int lngIndex;
            if (bStraightFound == true)
            {
                lngObjectInStraight = aObjectIndex!.GetUpperBound(0) - 1; // i think
                var loopTo = (long)Start + lngObjectInStraight;
                for (lngIndex = Start; lngIndex <= loopTo; lngIndex++)
                {
                    output.Add(firstList[lngIndex]);
                    if (output.Count == _maxStraight)
                    {
                        break;
                    }
                }
                int intHigh = default;
                int intLow = default;
                HighLow(ref firstList, output, howMany, minOnly, lngUnUsedWild, ref intHigh, ref intLow);
                FirstUsed = intLow;
                if (HasWild == true)
                {
                    var loopTo1 = wildList.Count() - 1;
                    for (lngIndex = 0; lngIndex <= loopTo1; lngIndex++)
                    {
                        if (lngIndex + 1 <= wildList.Count())
                        {
                            if (output.Count < howMany | minOnly == false)
                            {
                                output.Add(wildList[lngIndex]);
                            }
                        }
                        if (output.Count == _maxStraight)
                        {
                            return output;
                        }
                    }
                }
            }
            if (output.Count == objectList.Count & UseAll == false)
            {
                firstLinq = from Objects in output
                            where Objects.ReadMainValue == FirstUsed
                            select Objects;
                if (firstLinq.Count() > 0)
                {
                    output.RemoveSpecificItem(firstLinq.First());
                }
                else
                {
                    output.RemoveLastItem();
                }
            }
            aObjectIndex = null;
            int newnum;
            newnum = FirstUsed + output.Count - 1;
            newnum = HighNumber - newnum;
            if (FirstUsed > 0 & newnum < 0)
            {
                FirstUsed += newnum;
            }
            HasWild = tempwilds;
            return output;
        }
        private bool IsStraight()
        {
            bool output = default;
            bool bStraightFound;
            int[]? aObjectIndex;
            int lngUnUsedWild = default;
            UseSecond = false;
            int currentnum = default;
            int thismany = default;
            int x = default;
            CustomBasicList<R> filteredList;
            filteredList = (from Objects in _tempList
                            where Objects.IsObjectIgnored == false && Objects.IsObjectWild == false
                            select Objects).ToCustomBasicList();
            var loopTo = filteredList.Count - 1;
            for (x = 0; x <= loopTo; x++)
            {
                currentnum = filteredList[x].ReadMainValue;
                thismany = filteredList.Count(Items => Items.ReadMainValue == currentnum);
                if (thismany > 1)
                {
                    return false;
                }
            }
            CustomBasicList<R> newList = new CustomBasicList<R>();
            newList.AddRange(_tempList!);
            var wildList = newList.Where(Items => Items.IsObjectWild == true).ToCustomBasicList();
            _tempList = filteredList.ToCustomBasicList();
            if (NeedMatch == true)
            {
                _tempList = (from Items in _tempList
                             orderby Items.GetSuit ascending, Items.ReadMainValue ascending
                             select Items).ToCustomBasicList();
            }
            else
            {
                _tempList = (from Items in _tempList
                             orderby Items.ReadMainValue
                             select Items).ToCustomBasicList();
            }
            if (NeedMatch == true)
            {
                bool rets;
                rets = _tempList.HasOnlyOne(items => items.GetSuit);
                if (rets == false)
                {
                    return false;
                }
            }
            bStraightFound = false;
            FirstUsed = 0;
            aObjectIndex = new int[1];
            var argStartAt1 = 0;
            if (HasValidStraight(_tempList, wildList, false, _tempList.Count + wildList.Count, false, ref aObjectIndex, ref lngUnUsedWild, ref argStartAt1))
            {
                bStraightFound = true;
            }
            else
            {
                aObjectIndex = null;
                if (HasSecond == true)
                {
                    _tempList = _tempList.OrderBy(items => items.GetSecondNumber).ToCustomBasicList();
                    var argStartAt = 0;
                    if (HasValidStraight(_tempList, wildList, true, _tempList.Count + wildList.Count, false, ref aObjectIndex, ref lngUnUsedWild, ref argStartAt))
                    {
                        bStraightFound = true;
                        UseSecond = true;
                    }
                }
            }
            if (bStraightFound == true)
            {
                int lngCardInStraight;
                int lngIndex;
                CustomBasicList<R> straightset = new CustomBasicList<R>();
                lngCardInStraight = aObjectIndex!.GetUpperBound(0) - 1; // because its 0 based.
                var loopTo1 = lngCardInStraight;
                for (lngIndex = 0; lngIndex <= loopTo1; lngIndex++)
                {
                    straightset.Add(_tempList[lngIndex]);
                    if (straightset.Count == _maxStraight)
                    {
                        break;
                    }
                }
                int intHigh = default;
                int intLow = default;
                HighLow(ref _tempList, straightset, _tempList.Count, false, lngUnUsedWild, ref intHigh, ref intLow);
                FirstUsed = intLow;
            }
            aObjectIndex = null;
            output = bStraightFound;
            return output;
        }
        public CustomBasicList<R> WhatNewRummy(ICustomBasicList<R> objectList, int howMany, EnumRummyType whatRummy, bool minOnly, bool noWilds = false, bool minWilds = false)
        {
            CustomBasicList<R> output = new CustomBasicList<R>();
            PrivateNewRummy(output, objectList, howMany, whatRummy, minOnly, noWilds, minWilds);
            return output;
        }
        public DeckRegularDict<D> WhatNewRummy<D>(DeckRegularDict<D> cardList, int howMany, EnumRummyType whatRummy, bool minOnly, bool noWilds = false, bool minWilds = false)
            where D : IRummmyObject<S, C>, IDeckObject, new()
        {
            DeckRegularDict<D> output = new DeckRegularDict<D>();
            PrivateNewRummy(output, cardList, howMany, whatRummy, minOnly, noWilds, minWilds);
            return output;
        }
        private void PrivateNewRummy<RR>(ICustomBasicList<RR> output, ICustomBasicList<RR> objectList, int howMany, EnumRummyType whatRummy, bool minOnly, bool noWilds = false, bool minWilds = false)
            where RR : IRummmyObject<S, C>, new()
        {
            CustomBasicList<R> firstTemp = objectList.Cast<R>().ToCustomBasicList();
            GetTempList(firstTemp);
            CheckErrors();
            UseSecond = false;
            int wildsUsed = default;
            int maxWildsUsed;
            maxWildsUsed = 100;
            CustomBasicList<R> wildList;
            CustomBasicList<R> mainList = new CustomBasicList<R>();
            CustomBasicList<R> thisList;
            wildList = _tempList.Where(Items => Items.IsObjectWild == true).ToCustomBasicList();
            int WildsNeeded;
            int x;
            switch (whatRummy)
            {
                case EnumRummyType.Colors:
                    var colorList = _tempList.Where(Items => Items.IsObjectIgnored == false && Items.IsObjectWild == false).GroupBy(Items => Items.GetColor).ToCustomBasicList();
                    colorList.ForEach(thisColor =>
                    {
                        thisList = new CustomBasicList<R>();
                        int count = thisColor.Count();
                        if (count >= howMany)
                        {
                            thisList.AddRange(thisColor);
                            if (minOnly == false)
                            {
                                thisList.AddRange(wildList);
                            }
                        }
                        else if (minOnly == false & count + wildList.Count >= howMany & HasWild == true)
                        {
                            thisList.AddRange(thisColor);
                            thisList.AddRange(wildList);
                        }
                        else if (minOnly == true & HasWild == true & count + wildList.Count > howMany)
                        {
                            thisList.AddRange(thisColor);
                            WildsNeeded = howMany - count;
                            var loopTo = WildsNeeded - 1;
                            for (x = 0; x <= loopTo; x++)
                            {
                                thisList.Add(wildList[x]);
                            }
                        }
                        if (thisList.Count > mainList.Count)
                        {
                            mainList = thisList;
                        }
                    });
                    output.ReplaceRange(mainList.Cast<RR>());
                    if (output.Count == objectList.Count & UseAll == false)
                    {
                        output.RemoveAt(0);
                    }
                    return;
                case EnumRummyType.Sets:
                    int y;
                    var setList = _tempList.Where(Items => Items.IsObjectIgnored == false && Items.IsObjectWild == false).GroupBy(Items => Items.ReadMainValue).ToCustomBasicList();
                    setList.ForEach(thisSet =>
                    {
                        thisList = new CustomBasicList<R>();
                        int Count = thisSet.Count();
                        if (Count >= howMany)
                        {
                            thisList.AddRange(thisSet);
                            if (minOnly == true & thisList.Count > howMany)
                            {
                                y = thisList.Count - 1;
                                var loopTo1 = y;
                                for (x = howMany; x <= loopTo1; x++)
                                {
                                    thisList.RemoveAt(0);
                                }
                            }
                            if (minOnly == false & wildList.Count > 0 & minWilds == false) //they did not do else if here.  hopefully still okay
                            {
                                thisList.AddRange(wildList);
                                wildsUsed = 0;
                            }
                        }
                        else if ((minOnly == false | minWilds == true) & Count + wildList.Count >= howMany & HasWild == true)
                        {
                            thisList.AddRange(thisSet);
                            thisList.AddRange(wildList);
                            wildsUsed = wildList.Count;
                        }
                        else if (minOnly == true & HasWild == true & Count + wildList.Count > howMany)
                        {
                            thisList.AddRange(thisSet);
                            WildsNeeded = howMany - Count;
                            var loopTo2 = WildsNeeded;
                            for (x = 1; x <= loopTo2; x++)
                            {
                                thisList.Add(wildList[x - 1]);
                            }
                        }
                        if (wildsUsed < maxWildsUsed & HasWild == true & setList.Count() == mainList.Count & (minOnly == true | minWilds == true))
                        {
                            mainList = thisList;
                        }
                        else if (thisList.Count > mainList.Count & (HasWild == false | 
                        wildsUsed < maxWildsUsed & HasWild == true & (minOnly == true |
                        minWilds == true) |
                        HasWild == true & minOnly == false))
                        {
                            mainList = thisList;
                        }
                    });
                    output.ReplaceRange(mainList.Cast<RR>());
                    if (output.Count == objectList.Count & UseAll == false)
                    {
                        output.RemoveAt(0);
                    }
                    return;
                case EnumRummyType.Runs:
                    var ourObjects = objectList.Cast<R>().ToCustomBasicList();
                    var finTemp = StraightSet(ourObjects, howMany, minOnly, wildList, noWilds);
                    output.ReplaceRange(finTemp.Cast<RR>());
                    if (FirstUsed == 2 & HasSecond == true)
                    {
                        S suit;
                        var suitList = _tempList.Where(Items => Items.IsObjectIgnored == false 
                        && Items.IsObjectWild == false)
                            .GroupBy(Items => Items.GetSuit).ToCustomBasicList();
                        if (suitList.Count == 0)
                        {
                            throw new BasicBlankException("There are no suits available.  If there are really no suits; then fix this");
                        }
                        suit = suitList.First().Key;
                        var runTemp = objectList.Where(Items => Items.GetSuit.Equals(suit) && Items.GetSecondNumber == 1).FirstOrDefault();
                        if (runTemp != null)
                        {
                            output.InsertBeginning(runTemp); //maybe this should be inserted at beginning (?)
                        }
                    }
                    return;
                default:
                    {
                        throw new BasicBlankException("Cannot figure out what rummy type to do");
                    }
            }
        }
        public bool IsNewRummy(ICustomBasicList<R> objectList, int howMany, EnumRummyType whatRummy)
        {
            UseSecond = false;
            CheckErrors();
            if (objectList.Count() < howMany)
            {
                return false;
            }
            GetTempList(objectList);
            CustomBasicList<R> ignoreLinq = objectList.Where(Items => Items.IsObjectIgnored == true).ToCustomBasicList();
            if (ignoreLinq.Count > 0)
            {
                return false;
            }
            switch (whatRummy)
            {
                case EnumRummyType.Sets:
                    _tempList!.RemoveAllOnly(Items => Items.IsObjectWild == true);
                    return _tempList.HasOnlyOne(Items => Items.ReadMainValue);
                case EnumRummyType.Colors:
                    _tempList!.RemoveAllOnly(Items => Items.IsObjectWild == true);
                    return _tempList.HasOnlyOne(Items => Items.GetColor);
                case EnumRummyType.Runs:
                    return IsStraight();
                default:
                    throw new BasicBlankException("Not Supported");
            }
        }
        public bool CanCardUsed(ICustomBasicList<R> objectList, int whatElement, EnumRummyType whatRummy, int howMany = 3)
        {
            bool output = default;
            GetTempList(objectList);
            CheckErrors();
            if (objectList.Count < howMany)
            {
                output = false;
                return output;
            }
            int thisnum;
            S thissuit; //0 based
            thisnum = _tempList![whatElement].ReadMainValue;
            thissuit = _tempList[whatElement].GetSuit;
            int num1;
            int num2;
            S suit1;
            S suit2;
            CustomBasicList<R> newCols = new CustomBasicList<R>();
            _tempList = newCols.ToCustomBasicList(); // try this way.
            CustomBasicList<R> tempRummy;
            IEnumerable<R> firstLinq;
            IEnumerable<R> secondLinq;
            int x = 0;
            do
            {
                tempRummy = WhatNewRummy(newCols, howMany, whatRummy, false);
                if (tempRummy.Count == 0)
                {
                    return false;
                }
                firstLinq = from Objects in tempRummy
                            where Objects.ReadMainValue == thisnum & Objects.GetSuit.Equals(thissuit)
                            select Objects;
                if (firstLinq.Count() > 0)
                {
                    return true;
                }
                secondLinq = from Objects in tempRummy
                             where Objects.IsObjectIgnored == true && Objects.IsObjectWild == false
                             select Objects;
                foreach (R ThisObject in secondLinq)
                {
                    suit1 = ThisObject.GetSuit;
                    num1 = ThisObject.ReadMainValue;
                    var loopTo = _tempList.Count - 1;
                    for (x = 0; x <= loopTo; x++)
                    {
                        num2 = _tempList[x].ReadMainValue;
                        suit2 = _tempList[x].GetSuit;
                        if (num1 == num2 & suit1.Equals(suit2))
                        {
                            _tempList.RemoveAt(x);
                        }
                    }
                }
                newCols = new CustomBasicList<R>();
                _tempList = newCols.ToCustomBasicList();
            }
            while (true);
        }
    }
}