﻿
using LogXtreme.Ifrastructure.ContractValidators;
using LogXtreme.Infrastructure.ContractValidators;
using System;
using System.Collections.ObjectModel;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// 
    /// Refs
    /// https://stackoverflow.com/questions/4305623/how-to-resize-observablecollection
    /// https://stackoverflow.com/questions/2540825/c-sharp-public-enums-in-classes 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResizeObservableCollection<T> :
        ObservableCollection<T> {

        public enum CycleMode {
            Roll,
            Flush,
            Queue
        }

        private readonly int maxSize;
        private readonly CycleMode cycleMode;        

        public ResizeObservableCollection(
            int maxSize, 
            CycleMode cycleMode = CycleMode.Flush) {

            var validator = InvariantValidator.CreateValidator();

            validator.VerifyValue<ArgumentException>(
                maxSize, 
                0, 
                InvariantValidatorComparisonEnum.LargerThanOrEqual,
                $"{nameof(maxSize)} must be non negative intead is {maxSize}");

            //if (maxSize < 0) {
            //    throw new ArgumentException($"{nameof(maxSize)} must be non negative intead is {maxSize}");
            //}            

            this.maxSize = maxSize;
            this.cycleMode = cycleMode;
        }

        public int MaxSize => this.maxSize;

        public CycleMode Mode => this.cycleMode;

        protected override void InsertItem(int index, T item) {

            if (this.maxSize > 0 && this.Count == this.maxSize) {

                switch (this.cycleMode) {

                    case CycleMode.Roll:
                        this.RemoveAt(index-1);
                        base.InsertItem(0, item);
                        break;
                    case CycleMode.Flush:
                        this.Items.Clear();
                        base.InsertItem(0, item);
                        break;
                    case CycleMode.Queue:
                        this.RemoveAt(0);
                        base.InsertItem(this.Count, item);
                        break;
                    default:
                        break;
                }
            }
            else {

                base.InsertItem(index, item);
            }
        }
    }
}