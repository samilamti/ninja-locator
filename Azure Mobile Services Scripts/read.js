function read(query, user, request) {
    var ninjasTable = tables.getTable("ninja");
    ninjasTable.where({ UserId: user.userId }).read({
        success: function (results) {
            if (results.length > 0) {
                var existingNinja = results[0];
                query.where(function (userId, groupName) {
                    return this.GroupName == groupName && this.UserId != userId;
                }, existingNinja.UserId, existingNinja.GroupName);
            }
            request.execute();
        }
    });
}