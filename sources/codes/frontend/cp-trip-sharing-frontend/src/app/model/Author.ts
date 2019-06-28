export class Author {
    constructor() {
        const user = JSON.parse(localStorage.getItem('User'));
        this.id = user.id;
        this.displayName = user.displayName;
        this.profileImage = user.avatar;
    }
    // tslint:disable-next-line:variable-name
    id: string;
    // tslint:disable-next-line:variable-name
    displayName: string;
    // tslint:disable-next-line:variable-name
    profileImage: string;
}
