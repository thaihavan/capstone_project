export class Author {
    constructor() {
        const user = JSON.parse(localStorage.getItem('user'));
        this.id = user.id;
        this.displayName = user.displayName;
        this.profileImage = user.profileImage;
    }
    // tslint:disable-next-line:variable-name
    id: string;
    // tslint:disable-next-line:variable-name
    displayName: string;
    // tslint:disable-next-line:variable-name
    profileImage: string;
}
