export class PostFilter {
    // timePeriod is in [today, this_week, this_month, this_year, all_time]
    timePeriod: string;

    // topics is list of topicId
    topics: string[];

    // search by text
    search: string;

    // locationId of location in google map
    locationId: string;
}
