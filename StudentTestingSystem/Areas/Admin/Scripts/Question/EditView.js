document.getElementById('file').onchange = function () {
    var file = document.getElementById('file');
    if (file.files.length === 1) {
        document.getElementById('fileName').innerText = file.files[0].name;
    }
};

function addAnswer() {
    var answerIndex = getIndexForAnswer();
    if (answerIndex !== -1) {
        var answer = '<div id="wrongAnswer' + answerIndex + '" class="wrong-answer">' +
            '<input name="Answers[' + answerIndex + '].Id" type="hidden" value="00000000-0000-0000-0000-000000000000">' +
            '<input name="Answers[' + answerIndex + '].IsCorrectAnswer" type="hidden" value="false">' +
            '<div class="input-group mb-3">' +
            '<div class="input-group-prepend">' +
            '<span class="input-group-text" id="icon' + answerIndex + '"><i class="fas fa-times"></i></span>' +
            '</div>' +
            '<input class="form-control text-box single-line" id="Answers_' + answerIndex + '__Title" name="Answers[' + answerIndex + '].Title" aria-describedby="icon' + answerIndex + '" type="text" value="">' +
            '<div class="input-group-append">' +
            '<button class="btn btn-outline-danger btn-question" type="button" onclick="removeAnswer(' + answerIndex + ')" title="Delete answer"><i class="fas fa-trash-alt"></i></button>' +
            '</div>' +
            '</div>' +
            '</div>';
        $('#answers').append(answer);
        answers.push(answerIndex);
    }
}

function getIndexForAnswer() {
    var index = -1;
    if (answers.length < answerMaxCount) {
        for (var i = 0; i < answerMaxCount; i++) {
            if (answers.indexOf(i) === -1) {
                index = i;
                break;
            }
        }
    }
    return index;
}

function removeAnswer(id) {
    if (answers.length > 2) {
        $('#wrongAnswer' + id).remove();
        answers.splice(answers.indexOf(id), 1);
    }
}

function deleteImage() {
    $('#currentImage').remove();
    $('#deleteImage').modal('hide');
    $('#DeleteImage').val(true);
    $('#currentImageBigBlock').remove();
    removeNewImage();
}

function removeNewImage() {
    document.getElementById('fileName').innerText = 'Choose file...';
    var file = document.getElementById('file');
    if (file.files.length === 1) {
        file.value = '';
    }
}

function formValidation() {
    var errors = false;
    var errorsMessages = document.getElementById('errorsList');
    errorsMessages.innerHTML = '';

    var title = document.getElementById('Title').value.length;
    if (title < 3) {
        errors = true;
        var li = document.createElement('li');
        li.className = 'list-group-item';
        li.innerText = 'The question should be at least 3 characters long';
        errorsMessages.appendChild(li);
    }

    for (var i = 0; i < answers.length; i++) {
        var answer = document.getElementById('Answers_' + answers[i] + '__Title').value.length;
        if (answer === 0) {
            errors = true;
            var li = document.createElement('li');
            li.className = 'list-group-item';
            li.innerText = 'The answer should be at least 1 character long';
            errorsMessages.appendChild(li);
            break;
        }
    }

    var image = document.getElementById('file');
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