export class NgxChartInterface {
    view: number[];
    // options
    showXAxis: boolean;
    showYAxis: boolean;
    gradient: boolean;
    showLegend: boolean;
    legendPosition: string;
    showXAxisLabel: boolean;
    xAxisLabel: string;
    showYAxisLabel: boolean;
    yAxisLabel: string;

    xAxisTicks: any[];
    yAxisTicks: any[];
    xAxisTickFormatting: any;
    yAxisTickFormatting: any;
    timeline: boolean;

    colorScheme: any;

    // line, area
    autoScale: boolean;

    constructor() {
        this.showXAxis = true;
        this.showYAxis = true;
        this.gradient = false;
        this.showLegend = true;
        this.legendPosition = 'below';
        this.showXAxisLabel = false;
        this.xAxisLabel = 'X Axis Label';
        this.showYAxisLabel = false;
        this.yAxisLabel = 'Y Axis Label';
        this.timeline = false;

        this.colorScheme = {
            domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
        };

        this.autoScale = true;
    }
}
