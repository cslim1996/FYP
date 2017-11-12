using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamTimetabling2016
{
    public class MaintainConstraintSettingControl
    {
        private ConstraintSettingDA constraintSetting;

        public MaintainConstraintSettingControl()
        {
            constraintSetting = new ConstraintSettingDA();
        }

        public ConstraintSetting readSettingFromDatabase()
        {
            return constraintSetting.readSettingFromDatabase();
        }

        public void saveIntoDatabase(ConstraintSetting setting)
        {
            constraintSetting.saveIntoDatabase(setting);
        }

        public void shutDown()
        {
            constraintSetting.shutDown();
        }
    }
}