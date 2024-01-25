const NewSessionButton = document.getElementById("NewSessionButton");
const SessionEndButton = document.getElementById("SessionEndButton");

NewSessionButton.addEventListener("click", NewSession);
SessionEndButton.addEventListener("click",OnSessionEnd);

SessionEndButton.disabled = true;

function NewSession()
{
    var saveScorePoint = [5,15,35]
    var jsonData = 
    {
        "saveScorePoints" : saveScorePoint 
    }

    NewSessionButton.disabled = true;
    SessionEndButton.disabled = false;

    API.SendMessage('Api','SessionData',JSON.stringify(jsonData));
}



function OnSessionEnd(wonBonuses)
{
    var jsonData =
    {
        "wonBonuses" : wonBonuses
    }

    NewSessionButton.disabled = false;
    SessionEndButton.disabled = true;

    API.SendMessage('Api','SessionResult',JSON.stringify(jsonData));
}
