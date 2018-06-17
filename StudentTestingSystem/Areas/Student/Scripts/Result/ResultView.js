function drawMark(mark) {
    var barColor = bad;
    if (mark >= 4 && mark < 7) {
        barColor = middling;
    } else if (mark >= 7) {
        barColor = good;
    }

    var bar = new ProgressBar.Circle(markBar, {
        color: barColor,
        strokeWidth: 4,
        trailWidth: 1,
        easing: 'easeInOut',
        duration: 2300,
        text: {
            autoStyleContainer: false
        },
        from: { color: barColor, width: 4 },
        to: { color: barColor, width: 4 },
        step: function (state, circle) {
            circle.path.setAttribute('stroke', state.color);
            circle.path.setAttribute('stroke-width', state.width);

            var value = Math.round(circle.value() * 10);
            circle.setText(value);
        }
    });

    bar.text.style.fontFamily = '"Montserrat"';
    bar.text.style.fontSize = '3rem';

    var value = mark / 10;
    bar.animate(value);
}