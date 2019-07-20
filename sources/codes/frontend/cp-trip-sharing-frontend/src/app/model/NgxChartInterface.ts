export class NgxChartInterface {
    view: number[];
    // options
    showXAxis: boolean;
    showYAxis: boolean;
    gradient: boolean;
    showLegend: boolean;
    showXAxisLabel: boolean;
    xAxisLabel: string;
    showYAxisLabel: boolean;
    yAxisLabel: string;

    xAxisTicks: any[];
    yAxisTicks: any[];
    timeline: boolean;

    colorScheme: any;

    // line, area
    autoScale: boolean;

    constructor() {
        this.showXAxis = true;
        this.showYAxis = true;
        this.gradient = false;
        this.showLegend = true;
        this.showXAxisLabel = true;
        this.xAxisLabel = 'Number';
        this.showYAxisLabel = true;
        this.yAxisLabel = 'Color Value';
        this.timeline = false;

        this.colorScheme = {
            domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
        };

        this.autoScale = true;
    }
}
