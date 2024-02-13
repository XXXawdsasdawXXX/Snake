const NewSessionButton = document.getElementById("NewSessionButton");
const CloseButton = document.getElementById("CloseButton");
const MuteAudioButton = document.getElementById("MuteButton");
const UnMuteAudioButton = document.getElementById("UnMuteButton");

NewSessionButton.addEventListener("click", NewSession);
CloseButton.addEventListener("click",OnSessionEnd);
MuteAudioButton.addEventListener("click",MuteAudio);
UnMuteAudioButton.addEventListener("click",UnMuteAudio);

CloseButton.disabled = true;
UnMuteAudioButton.disabled = true;

function NewSession()
{
    var saveScorePoint = [5,15,35]
    var jsonData = 
    {
        "isMute" : false,
        "saveScorePoints" : saveScorePoint 
    }

    NewSessionButton.disabled = true;
    CloseButton.disabled = false;

    API.SendMessage('Api','SessionData',JSON.stringify(jsonData));
}

function MuteAudio()
{
    MuteAudioButton.disabled = true;
    UnMuteAudioButton.disabled = false;

    API.SendMessage('Api','MuteAudio');
}

function UnMuteAudio()
{
    MuteAudioButton.disabled = false;
    UnMuteAudioButton.disabled = true;

    API.SendMessage('Api','UnMuteAudio');
}


function OnSessionEnd(wonBonuses)
{
    NewSessionButton.disabled = false;
    CloseButton.disabled = true;

    console.log(`Session ended! Reward:${wonBonuses}`);
}
