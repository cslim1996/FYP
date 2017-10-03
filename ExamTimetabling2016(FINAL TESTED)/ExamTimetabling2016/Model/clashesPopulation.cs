using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_timetabling.classes
{
    public class clashesPopulation
    {
        public string courseID;
        public int clashAmount;
        public int populationAmount;
        public int score;

        public clashesPopulation(){

        }

        public clashesPopulation(String courseID, int clashAmount, int populationAmount)
        {
            this.courseID = courseID;
            this.clashAmount = clashAmount;
            this.populationAmount = populationAmount;
        }

        public String getCourseID()
        {
            return courseID;
        }

        public void setCourseID(String courseID)
        {
            this.courseID = courseID;
        }

        public int getClashAmount()
        {
            return clashAmount;
        }

        public void setClashAmount(int clashAmount)
        {
            this.clashAmount = clashAmount;
        }

        public int getPopulationAmount()
        {
            return populationAmount;
        }

        public void setPopulationAmount(int populationAmount)
        {
            this.populationAmount = populationAmount;
        }

        public int getScore()
        {
            return score;
        }

        public void setScore(int score)
        {
            this.score = score;
        }

        public int getPriorityScore(int lowestClash, int lowestPopulation)
        {
            return (( clashAmount / lowestClash ) + (populationAmount / lowestPopulation));
        }

        public String toString()
        {
            return "Clashes{" + "courseID=" + courseID + ", clashAmount=" + clashAmount + '}';
        }
    }
}