window.onload = function () {
    var chart = new CanvasJS.Chart("chartContainer", {
        title: {
            text: "Qualifications and Experience"
        },
        data: [{
            type: "column",
            dataPoints: [
                { y: 80, label: "HTML5" },
                { y: 90, label: "CSS3" },
                { y: 50, label: "JavaScript" },
                { y: 60, label: "jQuery" },
                { y: 70, label: "Bootstrap" },
                { y: 50, label: "Angular2" },
                { y: 65, label: "Asp.NET" },
                { y: 48, label: "PHP" },
                { y: 88, label: "Android" },
               
            ]
        }]
    });
    chart.render();
}