﻿
using LogXtreme.Infrastructure.ContractValidators;
using System;

namespace LogXtreme.WinDsk.Infrastructure.Models {

    /// <summary>
    /// An observable collection with resizable support.
    /// Refs
    /// https://stackoverflow.com/questions/4305623/how-to-resize-observablecollection
    /// https://stackoverflow.com/questions/2540825/c-sharp-public-enums-in-classes  
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResizeObservableCollection<T> :
        ObservableCollectionEx<T> {

        private readonly int maxSize;
        private readonly ResizeObservableCollectionCycleModeEnum cycleMode;

        public ResizeObservableCollection() {

            this.maxSize = 0;
            this.cycleMode = ResizeObservableCollectionCycleModeEnum.None;
        }

        public ResizeObservableCollection(
            int maxSize,
            ResizeObservableCollectionCycleModeEnum cycleMode = ResizeObservableCollectionCycleModeEnum.Queue) {

            maxSize.Validate(nameof(maxSize)).GreaterThanOrEqualTo(0);

            if (maxSize == 0 && cycleMode != ResizeObservableCollectionCycleModeEnum.None) {
                throw new ArgumentException($"wrong argument combination {nameof(maxSize)}={maxSize} and {nameof(cycleMode)}={cycleMode}");
            }

            if (cycleMode != ResizeObservableCollectionCycleModeEnum.None) {           
                maxSize.Validate(nameof(maxSize)).GreaterThan(0);
            }           

            this.maxSize = maxSize;
            this.cycleMode = cycleMode;
        }

        public int MaxSize => this.maxSize;

        public ResizeObservableCollectionCycleModeEnum Mode => this.cycleMode;

        protected override void InsertItem(int index, T item) {

            if (this.maxSize > 0 && this.Count == this.maxSize) {

                switch (this.cycleMode) {

                    case ResizeObservableCollectionCycleModeEnum.Roll:
                        this.RemoveAt(index - 1);
                        base.InsertItem(0, item);
                        break;
                    case ResizeObservableCollectionCycleModeEnum.Flush:
                        this.RemoveAll();
                        base.InsertItem(0, item);
                        break;
                    case ResizeObservableCollectionCycleModeEnum.Queue:
                        this.RemoveAt(0);
                        base.InsertItem(this.Count, item);
                        break;
                    case ResizeObservableCollectionCycleModeEnum.None:
                        throw new ArgumentException($"{nameof(ResizeObservableCollection<T>)} cannot insert item because cycle mode {this.cycleMode} and {nameof(ResizeObservableCollection<T>.MaxSize)} = {MaxSize}");
                    default:
                        throw new ArgumentException($"{nameof(ResizeObservableCollection<T>)} cannot insert item because cycle mode {this.cycleMode} is not an expected mode.");
                }
            }
            else {

                base.InsertItem(index, item);
            }
        }
    }
}
