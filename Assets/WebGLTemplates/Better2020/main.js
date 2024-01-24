const NewSessionButton = document.getElementById("NewSessionButton");
const RestoreSessionButton = document.getElementById("RestoreSessionButton");
const SuccessStepButton = document.getElementById("SuccessStepButton");
const FailStepButton = document.getElementById("FailStepButton");


NewSessionButton.addEventListener("click", NewSession);
RestoreSessionButton.addEventListener("click",RestoreSession);
SuccessStepButton.addEventListener("click",SuccessStep);
FailStepButton.addEventListener("click",FailStep);


SuccessStepButton.disabled = true;
FailStepButton.disabled = true;

var currentStepID = 1;

function NewSession()
{
    var jsonData = 
    {
        "StepID"    : 1,
        "prize"     : 10,
        "lastPrize" : 0
    }

    NewSessionButton.disabled = true;
    RestoreSessionButton.disabled = true;

    API.SendMessage('Api','SessionData',JSON.stringify(jsonData));
}

function RestoreSession()
{
    var jsonData = 
    {
        "StepID"    : 4,  //Три сундука открыто
        "prize"     : 40, //Текущий возможный выигрыш в случае успеха
        "lastPrize" : 30  //Последний выигрыш, который игрок может забрать
    }

    NewSessionButton.disabled = true;
    RestoreSessionButton.disabled = true;


    API.SendMessage('Api','SessionData',JSON.stringify(jsonData));
}

function SuccessStep()
{
   
    var jsonData = 
    {
        "StepID"    : currentStepID,
        "prize"     : currentStepID*10,
        "lastPrize" : (currentStepID-1)*10  
    }

    SuccessStepButton.disabled = true;
    FailStepButton.disabled = true;

    API.SendMessage('Api','SessionData',JSON.stringify(jsonData));
}

function FailStep()
{   
    var jsonData = 
    {
        "StepID"    : currentStepID,
        "prize"     : 0,
        "lastPrize" : 0  
    }

    NewSessionButton.disabled = false;

    SuccessStepButton.disabled = true;
    FailStepButton.disabled = true;

    API.SendMessage('Api','SessionData',JSON.stringify(jsonData));
}

function OnPlayerSelectedChest(jsonString)
{
    var jsonData = JSON.parse(jsonString);

    if(jsonData?.StepID)
    {
        currentStepID = jsonData.StepID;
        console.log(`Player Selected Chest! StepID:${jsonData.StepID}`);
    }

    SuccessStepButton.disabled = false;
    FailStepButton.disabled = false;
}

function OnSessionEnd()
{
    console.log("Session ended!");
 
    SuccessStepButton.disabled = true;
    FailStepButton.disabled = true;

    NewSessionButton.disabled = false;
}
