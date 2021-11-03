export default class BTableConfigStyle {
    public bordered: boolean;
    public striped: boolean;
    public narrowed: boolean;
    public hoverable: boolean;
    public mobileCards: boolean;
    public iconPack: string;

    constructor() {
        this.bordered = false;
        this.striped = false;
        this.narrowed = false;
        this.hoverable = true;
        this.mobileCards = true;
        this.iconPack = 'mdi';
    }
}