export class NotificationTemplates {

    // Notify: Like-post template
    getLikePostNotiTemplate(userDisplayName: string, postTitle: string) {
        return `<b>${userDisplayName}</b> đã thích bài viết <b>${postTitle}</b> của bạn`;
    }

    // Notify: Like-comment template
    getLikeCommentNotiTemplate(userDisplayName: string, postTitle: string) {
        return `<b>${userDisplayName}</b> đã thích một bình luận của bạn trong bài viết <b>${postTitle}</b>`;
    }

    // Notify: Comment to post template
    getCommentedNotiTemplate(userDisplayName: string, postTitle: string) {
        return `<b>${userDisplayName}</b> đã thêm một bình luận vào bài viết <b>${postTitle}</b> của bạn`;
    }

    // Notify: Reply to comment
    getReplyCommentNotiTemplate(userDisplayName: string, postTitle: string) {
        return `<b>${userDisplayName}</b> đã trả lời một bình luận của bạn trong bài viết <b>${postTitle}</b>`;
    }

    // Notify: Removed post
    getRemovedPostNotiTemplate(postTitle: string, reason: string) {
        return `Bài viết của bạn đã bị gỡ do <b>${reason}</b>`;
    }

    // Notify: Removed comment
    getRemovedCommentNotiTemplate(postTitle: string, reason: string) {
        return `Một bình luận của bạn trong bài viết <b>${postTitle}</b> đã bị gỡ do <b>${reason}</b>`;
    }

    // Notify: New join request
    getNewJoinRequestNotiTemplate(userDisplayName: string, postTitle: string) {
        return `<b>${userDisplayName}</b> đã gửi yêu cầu tham gia nhóm trong bài viết <b>${postTitle}</b>`;
    }

    // Notify: Join request accepted
    getJoinRequestAcceptedNotiTemplate(userDisplayName: string, postTitle: string) {
        return `<b>${userDisplayName}</b> đã chấp nhận yêu cầu tham gia nhóm của bạn trong bài viết <b>${postTitle}</b>`;
    }
}
