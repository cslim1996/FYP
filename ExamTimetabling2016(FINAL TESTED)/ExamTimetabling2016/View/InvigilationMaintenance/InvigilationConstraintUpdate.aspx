<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="InvigilationConstraintUpdate.aspx.cs" Inherits="ExamTimetabling2016.InvigilationConstraintUpdate" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Update Constraints</h1>
    <div style="height: 700px">
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

        </div>
        <div style="float: left; width: 440px; height: 300px; margin-right: 10px; text-align: center">
            <p>
                <b>Drag to Here</b>
            </p>
            <div class="boxB" id="boxB">
                <%--ondragenter="return dragEnter(event)"
            ondrop="return dragDrop(event)"
            ondragover="return dragOver(event)"--%>
            </div>
            <button id="clear">Clear</button>
            <div id="trash" style="float: left;">
                <img src="/../../images/trash-can-icon-61364.PNG" style="width: 80px; height: 80px;" />></div>

        </div>
        <div style="float: left; width: 150px; height: 300px; text-align: center">
            <p>
                <b>>Operator</b>
            </p>
            <div class="boxC" id="boxC">
                <div class="number" id="0">
                    0
                </div>
                <div class="number" id="1">
                    1
                </div>
                <div class="number" id="2">
                    2
                </div>
                <div class="number" id="3">
                    3
                </div>
                <div class="number" id="4">
                    4
                </div>
                <div class="number" id="5">
                    5
                </div>
                <div class="number" id="6">
                    6
                </div>
                <div class="number" id="7">
                    7
                </div>
                <div class="number" id="8">
                    8
                </div>
                <div class="number" id="9">
                    9
                </div>
                <div class="number" id=".">
                    .
                </div>
                <div class="operator" id="==">
                    ==
                </div>
                <div class="operator" id="!=">
                    !=
                </div>
                <div class="operator" id=">">
                    >
                </div>
                <div class="operator" id=">=">
                    >=
                </div>
                <div class="operator" id="<">
                    <
                </div>
                <div class="operator" id="<=">
                    <=
                </div>
                <div class="operator" id="&&">
                    AND
                </div>
                <div class="operator" id="||">
                    OR
                </div>
                <div class="operator" id="->">
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
    <script type="text/javascript">
        var existConstraint = <%= return_constraint %>;
        var id;
        var errorMessage = "";
        var errorLine = 1;
        var valid = true;
        var maintainIndex = 0;
        var variable=[];
        for (i = 1; i <= existConstraint.length; i++) {
            var e= $("<div>"+existConstraint[i-1].toString()+"</div>");
            $("#boxB").append(e);
            id= existConstraint[i-1].toString();
            if(id=="AND")
                id="&&";
            if(id=="OR")
                id="||";
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
            var isNumber =  /^\d+$/;
            if(existConstraint[i-1].match(isNumber)){
                e.attr("class","number");
            }
        }
        $("#clear").click(function () {
            $('#boxB').empty();
            return false;
        });

        $("#submit").click(function () {
            var list = document.getElementById("boxB").children;
            var linenumber=<%= return_line %>;
            var text = "";
            for (var i = 0; i < list.length; i++) {
                var checkString = list[i].getAttribute("class").substring(0, 5);
                if (i == 0) {
                    if (checkString == "varia") {
                        text += list[i].getAttribute("id").toString()+" ";
                        variable.push(list[i].getAttribute("id").toString());
                    }
                    else
                    {
                        errorMessage += errorLine + ". First item must be variable\n";
                        errorLine++;
                        valid = false;
                    }
                }
                else {
                    var compareString = list[i - 1].getAttribute("class").substring(0, 5);
                    if (((i + maintainIndex) % 2) == 0 && (checkString == "varia" || checkString == "numbe")) {
                        if (compareString == "opera") {
                            
                            text += list[i].getAttribute("id").toString()+" ";
                            if(checkString=="varia")
                                variable.push(list[i].getAttribute("id").toString());
                        }
                        else
                        {
                            errorMessage += errorLine + ". Number or variable must put after operator: variable1 >= variable2 AND variable3 < 5 \n";
                            errorLine++;
                            valid = false;
                        }
                    }
                    else if(checkString == "numbe" && compareString == "numbe"){
                        text = text.substring(0, text.length - 1);
                        text += list[i].getAttribute("id").toString()+" ";
                        maintainIndex--;
                    }
                    else if (((i + maintainIndex) % 2) == 1 && checkString == "opera") {
                        if (compareString == "varia" || compareString == "numbe") {
                            text += list[i].getAttribute("id").toString()+" ";
                        }
                        else
                        {
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
            if (i == 1) {
                errorMessage += errorLine + ". Incomplete Constraint\n";
                errorLine++;
                valid = false;
            }
            //if((list[list.length-1].getAttribute("class").substring(0,5) != "varia" && list[list.length-1].getAttribute("class").substring(0,5) != "numbe")){
            //    errorMessage += errorLine + ". Last item must be Variable or Number \n";
            //    errorLine++;
            //    valid = false;
            //}
            if (valid == true ) {
                text = text.substring(0, text.length - 1);
                var sorted_arr = variable.slice().sort();
                var result = [];
                var duplicate = false;
                var url = "InvigilationConstraintUpdate.aspx?LineUpdate="+text+"&lineNumber="+linenumber;
                $.ajax({
                    url: url,
                    type: 'POST',
                    data: {
                        'success': "yes",
                        'stringPass': text,
                        'linenumber': linenumber,
                        'arrVariable':variable.join(',')
                    },
                    success: function (html) {
                        alert(text);
                    },
                });
                
                                
            }
            
            else {
                alert(errorMessage);
                errorMessage = "";
                errorLine = 1;
            }

            $('#boxB').empty();
        });

        $("#boxA div").draggable({
            revert: "invalid",
            helper: "clone",
            containment: "frame",
        }
            );
        $("#boxC div").draggable({
            revert: "invalid",
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
            //drop: function (event, ui) {
            //    var idelt = $(ui.helper).clone().attr("class", $(this).attr("variable"));
            //    $("#boxB").append(idelt)
            //    idelt.draggable();
            //}
            drop: function (event, ui) {
                //var idelt = $(ui.draggable).clone().attr("class", "constraintVariable");
                if ($(ui.draggable).attr("class").substring(0, 5) == "opera") {
                    var idelt = $(ui.draggable).clone().attr("class", "operator");
                }
                else if ($(ui.draggable).attr("class").substring(0, 5) == "numbe") {
                    var idelt = $(ui.draggable).clone().attr("class", "number");
                }
                else  {
                    var idelt = $(ui.draggable).clone().attr("class", "variable");
                }
                //alert(idelt.attr("class"));
                //alert(ui.draggable.attr("class").substring(0, 5));
                //if (ui.draggable.attr("class").substring(0, 5) != "opera"
                //    || ui.draggable.attr("class").substring(0, 5) != "numbe"
                //    || ui.draggable.attr("class").substring(0, 5) != "varia"
                //    ) {
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
        //.mouseleave(function () {
        //    $("#span1").text("Move the mouse to variable, operator or number item to see the detail.");
        //    $("#span2").text("");
        //});
        $('#trash').droppable({
            accept: "#boxB div",
            drop: function (event, ui) {
                ui.draggable.remove();
            }
        });
        
    </script>
</asp:Content>

