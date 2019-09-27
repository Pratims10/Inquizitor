function func(x) {
    if ('speechSynthesis' in window){
        var synth = speechSynthesis;
        var flag = true;
        var str=x.toString();
        var playEle = document.getElementById('play'+str);
        var pauseEle = document.getElementById('pause'+str);
        var stopEle = document.getElementById("stop"+str);
        playEle.addEventListener('click', function(){
                 onClickPlay(x);
        },false);
        pauseEle.addEventListener('click', onClickPause);
        stopEle.addEventListener('click', onClickStop);
     
        function onClickPlay(x) {
            alert("qqq");
            if(flag){
                var str=x.toString();
                flag = true;
                utterance = new SpeechSynthesisUtterance(
                      document.getElementById(str).textContent);
                utterance.voice = synth.getVoices()[2];
                utterance.onend = function(){
                    flag = false;
                };
                synth.speak(utterance);
            }
            if(synth.paused) { /* unpause/resume narration */
                synth.resume();
            }
        }

          function onClickPause(){
                if(synth.speaking && !synth.paused){ /* pause narration */
                    synth.pause();
                }
            }
        function onClickStop(){
            if(synth.speaking)
            {
                flag = false;
                synth.cancel();
            }  
        }
    }
    else {
        
   alert("hgf");

    }
  }