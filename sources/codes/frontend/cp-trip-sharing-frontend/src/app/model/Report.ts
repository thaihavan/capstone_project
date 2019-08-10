import { User } from './User';
import { ReportType } from './ReportType';
import { Post } from './Post';
import { Comment } from './Comment';

export class Report {
    id: string;
    targetId: string;
    targetType: string;
    reportTypeId: string;
    content: string;
    date: Date;
    reporterId: string;
    isResolved: boolean;

    target: any;
    reportType: ReportType;
}
