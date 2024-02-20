const NewSessionButton = document.getElementById("NewSessionButton");
const MuteAudioButton = document.getElementById("MuteButton");
const UnMuteAudioButton = document.getElementById("UnMuteButton");

NewSessionButton.addEventListener("click", NewSession);
MuteAudioButton.addEventListener("click",MuteAudio);
UnMuteAudioButton.addEventListener("click",UnMuteAudio);

UnMuteAudioButton.disabled = true;

function NewSession()
{
    var saveScorePoint = [10,15,25]
    var jsonData = 
    {
        "isMute" : false,
        "saveScorePoints" : saveScorePoint 
    }
    
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
    console.log(`Session ended! Reward:${wonBonuses}`);
}
