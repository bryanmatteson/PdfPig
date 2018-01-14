﻿namespace UglyToad.PdfPig.ContentStream
{
    /// <summary>
    /// Used to uniquely identify and refer to objects in the PDF file.
    /// </summary>
    internal struct IndirectReference
    {
        /// <summary>
        /// A positive integer object number.
        /// </summary>
        public long ObjectNumber { get; }

        /// <summary>
        /// A non-negative integer generation number which starts as 0 and increases if the file is updated incrementally.
        /// </summary>
        public int Generation { get; }

        /// <summary>
        /// Create a new <see cref="IndirectReference"/>
        /// </summary>
        /// <param name="objectNumber">The object number.</param>
        /// <param name="generation">The generation number.</param>
        public IndirectReference(long objectNumber, int generation)
        {
            ObjectNumber = objectNumber;
            Generation = generation;
        }

        public override bool Equals(object obj)
        {
            if (obj is IndirectReference reference)
            {
                return reference.ObjectNumber == ObjectNumber
                       && reference.Generation == Generation;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 59;
                hash = hash * 97 + ObjectNumber.GetHashCode();
                hash = hash * 97 + Generation.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return $"{ObjectNumber} {Generation}";
        }
    }
}