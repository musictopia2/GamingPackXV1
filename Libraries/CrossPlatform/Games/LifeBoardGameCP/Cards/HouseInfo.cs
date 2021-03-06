﻿using LifeBoardGameCP.Data;
using System.Collections.Generic;
namespace LifeBoardGameCP.Cards
{
    public class HouseInfo : LifeBaseCard
    {
        public HouseInfo()
        {
            CardCategory = EnumCardCategory.House;
        }
        private EnumHouseType _houseCategory;
        public EnumHouseType HouseCategory
        {
            get { return _houseCategory; }
            set
            {
                if (SetProperty(ref _houseCategory, value))
                {
                    
                }
            }
        }
        public Dictionary<int, decimal> SellingPrices { get; set; } = new Dictionary<int, decimal>();
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
        private decimal _housePrice;
        public decimal HousePrice
        {
            get { return _housePrice; }
            set
            {
                if (SetProperty(ref _housePrice, value))
                {
                    
                }
            }
        }
        private decimal _insuranceCost;
        public decimal InsuranceCost
        {
            get { return _insuranceCost; }
            set
            {
                if (SetProperty(ref _insuranceCost, value))
                {
                    
                }
            }
        }
    }
}