import { User } from './User';
import { ReportType } from './ReportType';

export class ReportedUser {
    id: string;
    userId: string;
    reportTypeId: string;
    content: string;
    date: Date;
    reporterId: string;
    isResolved: boolean;

    user: User;
    reportType: ReportType;
}
