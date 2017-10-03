<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InvigilationConstraintAdd.aspx.cs" Inherits="ExamTimetabling2016.InvigilationConstraintAdd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Add Constraints</h1>
    <div style="height:700px">
        <div style="float: left; width: 240px; height: 300px; margin-right: 30px; text-align: center">
            <p><b>Variable</b></p>
            <div class="boxA" id="boxA">
                <div class="variable" id="VenueSize">
                    VenueSize
                </div>
                <div class="variable" id="numberOfPaper">
                    numberOfPaper
                </div>
                <div class="variable" id="invigilatorRequired">
                    invigilatorRequired
                </div>
                <div class="variable" id="duration">
                    duration
                </div>
                <div class="variable" id="roomUsed">
                    roomUsed
                </div>
                <div class="variable" id="Date">
                    Date
                </div>
                <div class="variable" id="period">
                    period
                </div>
                <div class="variable" id="isMuslim">
                    isMuslim
                </div>
                <div class="variable" id="gender">
                    gender
                </div>
                <div class="variable" id="numOfMale">
                    numOfMale
                </div>
                <div class="variable" id="numOfFemale">
                    numOfFemale
                </div>
                <div class="variable" id="PercentageOfExperienceInvigilator">
                    PercentageOfExperienceInvigilator
                </div>
                <div class="variable" id="PercentageOfExperienceReliefInvigilator">
                    PercentageOfExperienceReliefInvigilator
                </div>

            </div>
            <p id="referConstraint">Refer Constraint:</p>
            <select id="currentConstraint">
            </select>
        </div>
        <div style="float: left; width: 440px; height: 300px; margin-right: 10px; text-align: center">
            <p>
                <b>Drag to Here</b>
            </p>
            <div class="boxB" id="boxB">
            </div>
            <button id="clear">Clear</button>
            <div id="trash" style="float: left;">
                <img src="/../../images/trash-can-icon-61364.PNG" style="width: 80px; height: 80px;" />
            </div>


        </div>
        <div style="float: left; width: 150px; height: 300px;margin-right: 10px; text-align: center">
            <p><b>Operator</b></p>
            <div class="boxC" id="boxC">
                <div class="number" id="number1">
                    0
                </div>
                <div class="number" id="number2">
                    1
                </div>
                <div class="number" id="number3">
                    2
                </div>
                <div class="number" id="number4">
                    3
                </div>
                <div class="number" id="number5">
                    4
                </div>
                <div class="number" id="number6">
                    5
                </div>
                <div class="number" id="number7">
                    6
                </div>
                <div class="number" id="number8">
                    7
                </div>
                <div class="number" id="number9">
                    8
                </div>
                <div class="number" id="number10">
                    9
                </div>
                <div class="number" id="number11">
                    .
                </div>
                <div class="operator" id="operator1">
                    ==
                </div>
                <div class="operator" id="operator2">
                    !=
                </div>
                <div class="operator" id="operator3">
                    >
                </div>
                <div class="operator" id="operator4">
                    >=
                </div>
                <div class="operator" id="operator5">
                    <
                </div>
                <div class="operator" id="operator6">
                    <=
                </div>
                <div class="operator" id="operator7">
                    AND
                </div>
                <div class="operator" id="operator8">
                    OR
                </div>
                <div class="operator" id="operator9">
                    ->
                </div>
            </div>
            <button id="submit">Submit</button>
            <p id="p1">
                <span id="span1">Move the mouse to variable, operator or number item to see the detail.</span>
                <span id="span2"></span>
            </p>

        </div>
    </div>
    
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script>
        var currentConstraint=<%=return_currentConstraint%>;
        var dropDownList = document.getElementById("currentConstraint");
        for(i = 0; i < currentConstraint.length; i++){
            var option = document.createElement("option");
            option.text=currentConstraint[i];
            dropDownList.add(option);
        }

        $("#currentConstraint").change(function(){
            $('#boxB').empty();
            var existConstraint=[];
            $( "#currentConstraint option:selected" ).each(function() {
                
                existConstraint = $( this ).text().split(" ");
                for (i = 1; i <= existConstraint.length; i++) {
                    var e= $("<div>"+existConstraint[i-1].toString()+"</div>");
                    $("#boxB").append(e);
                    id= existConstraint[i-1].toString();
                    if(id=="AND")
                        id="&&";
                    if(id=="OR")
                        id="||";
                    switch (existConstraint[i-1].toString()) {
                        case "==":
                            id = "operator1"; break;
                        case "!=":
                            id = "operator2"; break;
                        case ">":
                            id = "operator3"; break;
                        case ">=":
                            id = "operator4"; break;
                        case "<":
                            id = "operator5"; break;
                        case "<=":
                            id = "operator6"; break;
                        case "&&":
                            id = "operator7"; break;
                        case "||":
                            id = "operator8"; break;
                        case "->":
                            id = "operator9"; break;
                        case "0":
                            id = "number1"; break;
                        case "1":
                            id = "number2"; break;
                        case "2":
                            id = "number3"; break;
                        case "3":
                            id = "number4"; break;
                        case "4":
                            id = "number5"; break;
                        case "5":
                            id = "number6"; break;
                        case "6":
                            id = "number7"; break;
                        case "7":
                            id = "number8"; break;
                        case "8":
                            id = "number9"; break;
                        case "9":
                            id = "number10"; break;
                        case ".":
                            id = "number11"; break;
                    }
                    e.attr("id",id);
                    switch (existConstraint[i-1].toString())
                    {
                        case "==":
                            e.attr("class","operator");
                            break;
                        case "!=":
                            e.attr("class","operator");
                            break;
                        case ">=":
                            e.attr("class","operator");
                            break;
                        case "<=":
                            e.attr("class","operator");
                            break;
                        case ">":
                            e.attr("class","operator");
                            break;
                        case "<":
                            e.attr("class","operator");
                            break;
                        case "OR":
                            e.attr("class","operator");
                            break;
                        case "AND":
                            e.attr("class","operator");
                            break;
                        case "->":
                            e.attr("class","operator");
                            break;
                        default:
                            e.attr("class","variable");
                    }
                    var isNumber =  /^\d*\.?\d*$/ ;
                    if(existConstraint[i-1].match(isNumber)){
                        e.attr("class","number");
                    }
                }
            });
        });
        $("#clear").click(function () {
            $('#boxB').empty();
            return false;
        });
        
        $("#submit").click(function () {
            var list = document.getElementById("boxB").children;
            var text = "";
            var errorMessage = "";
            var errorLine = 1;
            var valid = true;
            var maintainIndex = 0;
            var variable = []
            for (var i = 0; i < list.length; i++) {
                var checkString = list[i].getAttribute("class").substring(0, 5);
                //check first item must be variable
                if (i == 0) {

                    if (checkString == "varia") {
                        text += list[i].getAttribute("id").toString() + " ";
                        variable.push(list[i].getAttribute("id").toString());
                    }
                    else {
                        errorMessage += errorLine + ". First item must be variable\n";
                        errorLine++;
                        valid = false;
                    }
                }
                else {
                    var compareString = list[i - 1].getAttribute("class").substring(0, 5);
                    //even number must be variable or number
                    if (((i + maintainIndex) % 2) == 0 && (checkString == "varia" || checkString == "numbe")) {
                        if (compareString == "opera") {
                            switch (list[i].getAttribute("id")) {
                                case "roomUsed":
                                    text += "roomUsed ";
                                    variable.push("roomUsed");
                                    break;
                                case "numberOfPaper":
                                    text += "numberOfPaper ";
                                    variable.push("numberOfPaper");
                                    break;
                                case "invigilatorRequired":
                                    text += "invigilatorRequired ";
                                    variable.push("invigilatorRequired");
                                    break;
                                case "duration":
                                    text += "duration ";
                                    variable.push("duration");
                                    break;
                                case "VenueSize":
                                    text += "VenueSize ";
                                    variable.push("VenueSize");
                                    break;
                                case "gender":
                                    text += "gender ";
                                    variable.push("gender");
                                    break;
                                case "isMuslim":
                                    text += "isMuslim ";
                                    variable.push("isMuslim");
                                    break;
                                case "period":
                                    text += "period ";
                                    variable.push("period");
                                    break;
                                case "Date":
                                    text += "Date ";
                                    variable.push("Date");
                                    break;
                                case "numOfMale":
                                    text += "numOfMale ";
                                    variable.push("numOfMale");
                                    break;
                                case "numOfFemale":
                                    text += "numOfFemale ";
                                    variable.push("numOfFemale");
                                    break;
                                case "PercentageOfExperienceInvigilator":
                                    text += "PercentageOfExperienceInvigilator ";
                                    variable.push("PercentageOfExperienceInvigilator");
                                    break;
                                case "PercentageOfExperienceReliefInvigilator":
                                    text += "PercentageOfExperienceReliefInvigilator ";
                                    variable.push("PercentageOfExperienceReliefInvigilator");
                                    break;
                                case "number1":
                                    text += "0 ";
                                    break;
                                case "number2":
                                    text += "1 "; break;
                                case "number3":
                                    text += "2 "; break;
                                case "number4":
                                    text += "3 "; break;
                                case "number5":
                                    text += "4 "; break;
                                case "number6":
                                    text += "5 "; break;
                                case "number7":
                                    text += "6 "; break;
                                case "number8":
                                    text += "7 "; break;
                                case "number9":
                                    text += "8 "; break;
                                case "number10":
                                    text += "9 "; break;
                                case "number11":
                                    text += ". "; break;
                            }
                        }
                        else {
                            errorMessage += errorLine + ". Number or variable must put after operator: variable1 >= variable2 AND variable3 < 5 \n";
                            errorLine++;
                            valid = false;
                        }
                    }
                        //this is for number combination eg:12,100,4231
                    else if (checkString == "numbe" && compareString == "numbe") {
                        text = text.substring(0, text.length - 1);
                        switch (list[i].getAttribute("id")) {
                            case "number1":
                                text += "0 ";
                                break;
                            case "number2":
                                text += "1 "; break;
                            case "number3":
                                text += "2 "; break;
                            case "number4":
                                text += "3 "; break;
                            case "number5":
                                text += "4 "; break;
                            case "number6":
                                text += "5 "; break;
                            case "number7":
                                text += "6 "; break;
                            case "number8":
                                text += "7 "; break;
                            case "number9":
                                text += "8 "; break;
                            case "number10":
                                text += "9 "; break;
                            case "number11":
                                text += ". "; break;

                        }
                        maintainIndex--;
                    }
                        //odd number must be operator
                    else if (((i + maintainIndex) % 2) == 1 && checkString == "opera") {
                        if (compareString == "varia" || compareString == "numbe") {
                            switch (list[i].getAttribute("id")) {
                                case "operator1":
                                    text += "== "; break;
                                case "operator2":
                                    text += "!= "; break;
                                case "operator3":
                                    text += "> "; break;
                                case "operator4":
                                    text += ">= "; break;
                                case "operator5":
                                    text += "< "; break;
                                case "operator6":
                                    text += "<= "; break;
                                case "operator7":
                                    text += "&& "; break;
                                case "operator8":
                                    text += "|| "; break;
                                case "operator9":
                                    text += "-> "; break;
                            }
                        }
                        else {
                            errorMessage += errorLine + ". Operator must put between variable or number, Example: variable1 >= variable2 AND variable3 < 5 \n";
                            errorLine++;
                            valid = false;
                        }
                    }
                    else {
                        errorMessage += errorLine + ". position of item is incorrect\n";
                        errorLine++;
                        valid = false;
                    }
                }
                if (valid == false) {
                    break;
                }
            }
            //check the condition of variable
            var checkVariable = text.substring(0, text.length-1).split(" ");
            
            for (var i = 0; i < checkVariable.length; i++) {
                switch(checkVariable[i]){
                    case "PercentageOfExperienceReliefInvigilator":
                        if (checkVariable[i+2] > 1 || checkVariable[i+2] < 0)
                        {
                            
                            errorMessage += errorLine+". input of PercentageOfExperienceReliefInvigilator must between 0 to 1\n";
                            errorLine++;
                            valid = false;
                        }
                        break;
                    case "VenueSize":
                        if ( checkVariable[i+2] > 5 || checkVariable[i+2] < 0)
                        {
                            errorMessage += errorLine+". input of VenueSize must between 0 to 5\n";
                            errorLine++;
                            valid = false;
                        }
                        break;
                    case "Date":
                        if (checkVariable[i+2] > 7 || checkVariable[i+2] < 1)
                        {
                            errorMessage += errorLine+". Input of Date must between 1 to 7\n";
                            errorLine++;
                            valid = false;
                        }
                        break;
                    case "PercentageOfExperienceInvigilator":
                        if (checkVariable[i+2] > 1 || checkVariable[i+2] < 0)
                        {
                            valid = false;
                            errorMessage += errorLine+". input of PecentageOfExperienceInvigilator must between 0 to 1\n";
                            errorLine++;
                        }
                        break;
                    case "period":
                        if (checkVariable[i+2] > 3 || checkVariable[i+2] < 1)
                        {
                            valid = false;
                            errorMessage += ". input of period must between 1 to 3\n";
                            errorLine++;
                        }
                        break;
                    case "isMuslim":
                        if (checkVariable[i+2] > 2 || checkVariable[i+2] < 1)
                        {
                            valid = false;
                            errorMessage += ". input of isMuslim must between 1 to 2\n";
                            errorLine++;
                        }
                        break;
                    case "gender":
              
                        if (checkVariable[i+2] > 2 || checkVariable[i+2] < 1)
                        {
                            valid = false;
                            errorMessage += ". input of gender must between 1 to 2\n";
                            errorLine++;
                        }
                        break;
                }
                
            }
            
            if(i==1){
                errorMessage += errorLine + ". Incomplete Constraint \n";
                errorLine++;
                valid = false;
            }
            //ValidFinalResult
            if (valid == true  ) {
                
                text = text.substring(0, text.length - 1);
                var sorted_arr = variable.slice().sort();
                var result = [];
                //compare duplicate
                var url = "InvigilationConstraintAdd.aspx";
                var success="yes";
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: {
                        'success': success,
                        'stringPass': text,
                        'arrVariable': variable.join(',')
                    },
                    success: function (data) {
                        alert(text);
                    },

                });
            }
            else{
                alert(errorMessage);
                errorMessage = "";
                errorLine = 1;
            }
            $('#boxB').empty();
        });

        $("#boxA div").draggable({
            revert: "invalid",
            cursor: "move",
            helper: "clone",
            containment: "frame",
        }
            );
        $("#boxC div").draggable({
            revert: "invalid",
            cursor: "move",
            helper: "clone",
            containment: "frame",
        }
            );

        $("#boxB").sortable({
            revert: false,
            cursor: "move",

        });

        $("#boxB").droppable({
            tolerance: "fit",
            accept: "#boxA div , #boxC div",
            drop: function (event, ui) {
                if ($(ui.draggable).attr("class").substring(0, 5) == "opera") {
                    var idelt = $(ui.draggable).clone().attr("class", "operator");
                }
                else if ($(ui.draggable).attr("class").substring(0, 5) == "numbe") {
                    var idelt = $(ui.draggable).clone().attr("class", "number");
                }
                else {
                    var idelt = $(ui.draggable).clone().attr("class", "variable");
                }
                $("#boxB").append(idelt);
                //

            }
        });

        $("#numberOfPaper")
            .mouseenter(function () {
                $("#span1").text("NumberofPaper is represent how many paper in a venue.");
                $("#span2").text("Example: numberOfPaper > 5 mean number of paper in a venue is more than 5");

            });
        $("#VenueSize")
            .mouseenter(function () {
                $("#span1").text("VenueSize is represent size of venue. Range of venue is between 0 to 5");
                $("#span2").text("Example: VenueSize == 0 represent venue of block H,M,PA,Q,R,V,L1,L3,SD,SE \n         venuesize == 1 represent block L2\n         venuesize == 2 represent block SB1,SB3\n         venuesize == 3 represent block SB2,SB4\n         venuesize == 4 represent block DU\n         venuesize == 5 represent block KS1,KS2");
                $("#span2").html($("#span2").html().replace(/\n/g, '<br/>'));
            });
        $("#invigilatorRequired")
            .mouseenter(function () {
                $("#span1").text("invigilatorRequired is represent how many invigilator required in the room");
                $("#span2").text("Example: VenueSize == 1 -> invigilatorRequired = 2 mean required 2 invigilator in each venue of block L2");
                $("#span2").html($("#span2").html().replace(/\n/g, '<br/>'));
            });
        $("#duration")
            .mouseenter(function () {
                $("#span1").text("duration is represent duration of paper");
                $("#span2").text("Example: duration == 1 mean duration paper is 1 ");
            });
        $("#Date")
            .mouseenter(function () {
                $("#span1").text("date is represent day of week. Range of venue is between 1 to 7");
                $("#span2").text("Example: date == 5 is represent friday");
            });
        $("#period")
            .mouseenter(function () {
                $("#span1").text("period is represent session of paper. Range of venue is between 1 to 3");
                $("#span2").text("Example: period == 1 is represent morning paper\n         period == 2 is represent afternoon paper\n         period == 3 is represent evening paper");
                $("#span2").html($("#span2").html().replace(/\n/g, '<br/>'));
            });
        $("#roomUsed")
            .mouseenter(function () {
                $("#span1").text("roomUsed is represent required of room used in Block H will be divided to 2 block");
                $("#span2").text("Example: roomUsed == 10 is represent if 10 venue of room is used in block H, block H will be divided into 2 block");
            });
        $("#isMuslim")
            .mouseenter(function () {
                $("#span1").text("isMuslim is represent whether invigilator is muslin. Range of venue is between 1 to 2");
                $("#span2").text("Example: isMuslim == 1 is represent muslim invigilator\n         isMuslim == 2 is represent non-muslim invigilator");
                $("#span2").html($("#span2").html().replace(/\n/g, '<br/>'));
            });
        $("#gender")
            .mouseenter(function () {
                $("#span1").text("gender is represent invigilator gender. Range of venue is between 1 to 2");
                $("#span2").text("Example: gender == 1 is represent male invigilator\n         gender == 2 is represent female invigilator");
                $("#span2").html($("#span2").html().replace(/\n/g, '<br/>'));
            });
        $("#numOfMale")
            .mouseenter(function () {
                $("#span1").text("numOfMale is represent how many male relief invigilator is required");
                $("#span2").text("Example: numOfMale == 2 is represent required 2 relief invigilator in one session");
            });
        $("#numOfFemale")
            .mouseenter(function () {
                $("#span1").text("numOfFemae is represent how many female relief invigilator is required");
                $("#span2").text("Example: numOfFemae == 1 is represent required 2 relief invigilator in one session");
            });
        $("#PercentageOfExperienceInvigilator")
            .mouseenter(function () {
                $("#span1").text("PercentageOfExperienceInvigilator is represent how many percentage of experience invigilator is required in location DU and KS. Range of venue is between 0 to 1 and it is able for decimal number");
                $("#span2").text("Example: PercentageOfExperienceInvigilator > 0.5 is represent experience invigilator must at least 50% in one venue");
            });
        $("#PercentageOfExperienceReliefInvigilator")
            .mouseenter(function () {
                $("#span1").text("PercentageOfExperienceReliefInvigilator is represent how many percentage of relief experience invigilator is required in one session. Range of venue is between 0 to 1 and it is able for decimal number");
                $("#span2").text("Example: PercentageOfExperienceReliefInvigilator > 0.5 is represent experience invigilator must at least 50% in one venue");
            });
        $('#trash').droppable({
            accept: "#boxB div",
            drop: function (event, ui) {
                ui.draggable.remove();
            }
        });
    </script>
</asp:Content>
