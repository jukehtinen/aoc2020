
local player1 = {}
local player2 = {}
local current = nil
for line in io.lines("input.txt") do
    local a = string.match(line, "Player (%d)")
    if a == '1' then current = player1 end;
    if a == '2' then current = player2 end;
    if a == nil and #line > 0 then
        table.insert(current, tonumber(line));
    end

end

while #player1 > 0 and #player2 > 0 do
    if player1[1] > player2[1] then
        table.insert(player1, table.remove(player1, 1))
        table.insert(player1, table.remove(player2, 1))
        current = player1
    else
        table.insert(player2, table.remove(player2, 1))
        table.insert(player2, table.remove(player1, 1))
        current = player2
    end
end

local result = 0
for i, v in ipairs(current) do
    result = result + (#current - i + 1) * v
end
print("Part 1 ", result)
