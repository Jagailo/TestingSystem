$('input').on("click", function () {
    var selectedAnswerId = $('input[name=answers]:checked').val();
    document.getElementById('selectedAnswerId').value = selectedAnswerId;
});

function formatNumber(number) {
    if (number < 10) {
        return '0' + number;
    } else {
        return '' + number;
    }
}

function startTick() {
    document.getElementById('secRemaining').innerText = formatNumber(secondsCounter);
    document.getElementById('minRemaining').innerText = formatNumber(parseInt(remainingSeconds % 3600 / 60));
    document.getElementById('hourRemaining').innerText = formatNumber(parseInt(remainingSeconds / 3600));

    var tick = setInterval(function () {
        if (secondsCounter === 0) {
            secondsCounter = 60;
        }        

        if (remainingSeconds > 0) {
            remainingSeconds--;
            secondsCounter--;

            document.getElementById('secRemaining').innerText = formatNumber(secondsCounter);
            document.getElementById('minRemaining').innerText = formatNumber(parseInt(remainingSeconds % 3600 / 60));
            document.getElementById('hourRemaining').innerText = formatNumber(parseInt(remainingSeconds / 3600));

            // TODO: disable button, when second === 1
            //} else if (remainingSeconds === 1){
            //    document.getElementById("recordAnswer").disabled = true;
            //    document.getElementById("submitTest").disabled = true;
            //    $('.choiceOptions link').attr('disabled', 'disabled');
        } else {
            clearInterval(tick);
            var createResultRequest = {
                ThemeId: $('#themeId').val()
            };

            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: ajaxURL,
                data: JSON.stringify(createResultRequest),
                success: function (data) {
                    window.location.href = data;
                },
                error: function (xhr, err) {
                    alert('readyState: ' + xhr.readyState + '\nstatus: ' + xhr.status);
                    alert('responseText: ' + xhr.responseText);
                }
            });
        }
    }, 1000);
}