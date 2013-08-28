using System;
using System.Collections.Generic;
using System.Text;

namespace IFOProject.DataStructures
{
    public class Coefficients
    {
        /// <summary>
        /// Mean level
        /// </summary>
        public double MeanLevel { get; set; }
        /// <summary>
        /// Amplitude
        /// </summary>
        public double Amplitude { get; set; }
        /// <summary>
        /// Period
        /// </summary>
        public double Period { get; set; }
        /// <summary>
        /// Initial phase
        /// </summary>
        public double InitialPhase { get; set; }

        public Coefficients() { }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="that">Another object</param>
        public Coefficients(Coefficients that)
        {
            this.MeanLevel = that.MeanLevel;
            this.Amplitude = that.Amplitude;
            this.Period = that.Period;
            this.InitialPhase = that.InitialPhase;
        }

        /// <summary>
        /// Creates object from array
        /// </summary>
        /// <param name="values">Coefficients array</param>
        public Coefficients(double[] values)
        {
            this.MeanLevel = values[0];
            this.Amplitude = values[1];
            this.Period = values[2];
            this.InitialPhase = values[3];
        }

        /// <summary>
        /// Returns coefficients array
        /// </summary>
        /// <returns>Coefficients array</returns>
        public double[] ToArray()
        {
            return new double[] { MeanLevel, Amplitude, Period, InitialPhase };
        }

        public static Coefficients operator *(Coefficients a, double mult)
        {
            Coefficients b = new Coefficients(a);
            b.MeanLevel *= mult;
            b.Amplitude *= mult;
            b.Period *= mult;
            b.InitialPhase *= mult;
            return b;
        }

        public static Coefficients operator -(Coefficients a, Coefficients b)
        {
            Coefficients c = new Coefficients(a);
            c.MeanLevel -= b.MeanLevel;
            c.Amplitude -= b.Amplitude;
            c.Period -= b.Period;
            c.InitialPhase -= b.InitialPhase;
            return c;
        }

        public static void Swap(Coefficients a, Coefficients b)
        {
            Coefficients c = new Coefficients(a);
            a = b;
            b = c;
        }
    }
}
