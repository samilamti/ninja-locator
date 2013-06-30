function insert(item, user, request) {
    // augment existing item
    item.LastSeen = new Date();
    item.UserId = user.userId;
    item.GroupName = item.GroupName.toUpperCase();

    var ninjasTable = tables.getTable("ninja");
    ninjasTable.where({ UserId: user.userId }).read({
        success: function (results) {
            if (results.length > 0) {
                // update existing ninja
                var existingNinja = results[0];
                existingNinja.NickName = item.NickName;
                existingNinja.GroupName = item.GroupName;
                existingNinja.Latitude = item.Latitude;
                existingNinja.Longitude = item.Longitude;
                existingNinja.LastSeen = item.LastSeen;
                ninjasTable.update(existingNinja);
                request.respond(statusCodes.OK);
            }
            else {
                // insert new ninja
                request.execute();
            }
        }
    });
}