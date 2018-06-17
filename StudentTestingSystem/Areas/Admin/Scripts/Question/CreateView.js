var answers = [];

document.getElementById('image').onchange = function () {
    var file = document.getElementById('image');
    if (file.files.length === 1) {
        document.getElementById('fileName').innerText = file.files[0].name;
    }
};

function addAnswer() {
    var answerIndex = getIndexForAnswer();
    if (answerIndex !== -1) {
        var answer = '<div id="answer_' + answerIndex + '">' +
            '<div class="input-group mb-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text" id="icon' + answerIndex + '"><i class="fas fa-times"></i></span>' +
            '</div>' +
            '<input id="answer' + answerIndex + '" class="form-control" name="answer-' + answerIndex + '-title" type="text" aria-describedby="icon' + answerIndex + '" value="">' +
            '<input name="answer-' + answerIndex + '-bool" type="hidden" value="false">' +
            '<div class="input-group-append">' +
            '<button class="btn btn-outline-danger btn-question" type="button" onclick="removeAnswer(' + answerIndex + ')" title="Delete answer"><i class="fas fa-trash-alt"></i></button>' +
            '</div>' +
            '</div>' +
            '</div>';
        $('#incorrectAnswers').append(answer);
        answers.push(answerIndex);
    }
}

function getIndexForAnswer() {
    var index = -1;
    for (var i = 2; answers.length + 2 < answerMaxCount; i++) {
        if (answers.indexOf(i) === -1) {
            index = i;
            break;
        }
    }
    return index;
}

function removeAnswer(id) {
    $('#answer_' + id).remove();
    answers.splice(answers.indexOf(id), 1);
}

function removeImage() {
    document.getElementById('fileName').innerText = 'Choose file...';
    var file = document.getElementById('image');
    if (file.files.length === 1) {
        file.value = '';
    }
}

function formValidation() {
    var errors = false;
    var errorsMessages = document.getElementById('errorsList');
    errorsMessages.innerHTML = '';

    var title = document.getElementById('title').value.length;
    if (title < 3) {
        errors = true;
        var li = document.createElement('li');
        li.className = 'list-group-item';
        li.innerText = 'The question should be at least 3 characters long';
        errorsMessages.appendChild(li);
    }

    var allAnswers = JSON.parse(JSON.stringify(answers));
    allAnswers.push(0);
    allAnswers.push(1);
    for (var i = 0; i < allAnswers.length; i++) {
        var answer = document.getElementById('answer' + allAnswers[i]).value.length;
        if (answer === 0) {
            errors = true;
            var li = document.createElement('li');
            li.className = 'list-group-item';
            li.innerText = 'The answer should be at least 1 character long';
            errorsMessages.appendChild(li);
            break;
        }
    }

    var image = document.getElementById('image');
    if (image.files.length === 1 && image.files[0].size > 3145728) { // 3 MB
        errors = true;
        var li = document.createElement('li');
        li.className = 'list-group-item';
        li.innerText = 'The size of the uploaded image shouldn\'t exceed 3 MB';
        errorsMessages.appendChild(li);
    }

    if (errors) {
        document.getElementById('formErrors').style.display = 'block';
        return false;
    }
    return true;
}