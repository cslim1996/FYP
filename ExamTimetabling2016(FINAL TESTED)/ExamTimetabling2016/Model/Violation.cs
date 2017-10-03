using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_timetabling.classes
{
    public class Violation
    {
        public int totalViolation;
        public int h1Violation;
        public int h2Violation;
        public int h3Violation;
        public int h4Violation;
        public int h5Violation;
        public int h6Violation;
        public int h9Violation;
        public int h10Violation;
        public int h11Violation;
        public int h12Violation;
        public int h13Violation;

        public Violation(){
        }

        public Violation(int h1Violation, int h2Violation, int h3Violation, int h4Violation, int h5Violation, int h6Violation, int h9Violation, int h10Violation, int h11Violation, int h12Violation, int h13Violation)
        {
            this.h1Violation = h1Violation;
            this.h2Violation = h2Violation;
            this.h3Violation = h3Violation;
            this.h4Violation = h4Violation;
            this.h5Violation = h5Violation;
            this.h6Violation = h6Violation;
            this.h9Violation = h9Violation;
            this.h10Violation = h10Violation;
            this.h11Violation = h11Violation;
            this.h12Violation = h12Violation;
            this.h13Violation = h13Violation;
        }

        public int getTotalViolation() {
            return h1Violation + h2Violation + h3Violation + h4Violation + h5Violation + h6Violation + h9Violation + h10Violation + h11Violation + h12Violation + h13Violation;
        }

        public void setTotalViolation(int totalViolation) {
            this.totalViolation = totalViolation;
        }

        public int getH1Violation() {
            return h1Violation;
        }

        public void setH1Violation(int h1Violation) {
            this.h1Violation = h1Violation;
        }

        public int getH2Violation() {
            return h2Violation;
        }

        public void setH2Violation(int h2Violation) {
            this.h2Violation = h2Violation;
        }

        public int getH3Violation() {
            return h3Violation;
        }

        public void setH3Violation(int h3Violation) {
            this.h3Violation = h3Violation;
        }

        public int getH4Violation() {
            return h4Violation;
        }

        public void setH4Violation(int h4Violation) {
            this.h4Violation = h4Violation;
        }

        public int getH5Violation() {
            return h5Violation;
        }

        public void setH5Violation(int h5Violation) {
            this.h5Violation = h5Violation;
        }

        public int getH6Violation() {
            return h6Violation;
        }

        public void setH6Violation(int h6Violation) {
            this.h6Violation = h6Violation;
        }

        public int getH9Violation() {
            return h9Violation;
        }

        public void setH9Violation(int h9Violation) {
            this.h9Violation = h9Violation;
        }

        public int getH10Violation() {
            return h10Violation;
        }

        public void setH10Violation(int h10Violation) {
            this.h10Violation = h10Violation;
        }

        public int getH11Violation() {
            return h11Violation;
        }

        public void setH11Violation(int h11Violation) {
            this.h11Violation = h11Violation;
        }

        public int getH12Violation() {
            return h12Violation;
        }

        public void setH12Violation(int h12Violation) {
            this.h12Violation = h12Violation;
        }

        public int getH13Violation() {
            return h13Violation;
        }

        public void setH13Violation(int h13Violation) {
            this.h13Violation = h13Violation;
        }

    }
}