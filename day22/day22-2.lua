
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

function CopyDeck(deck, size)
    local new = {}
    for i=1, size do
        new[i] = deck[i]
    end
    return new
end

function DeckHash(deck1, deck2)
    local result = 0
    for i, v in ipairs(deck1) do
        result = result + (#deck1 - i + 1) * v
    end
    local result2 = 0
    for i, v in ipairs(deck2) do
        result2 = result2 + (#deck2 - i + 1) * v
    end
    return tostring(result) .. ":" .. tostring(result2)
end

function PlayGame(deck1, deck2)
    local played = {}

    while #deck1 > 0 and #deck2 > 0 do
        local deckhash = DeckHash(deck1, deck2)
        if played[deckhash] ~= nil then return true end
        played[deckhash] = true

        local p1 = table.remove(deck1, 1)
        local p2 = table.remove(deck2, 1)
        if p1 <= #deck1 and p2 <= #deck2 then
            if PlayGame(CopyDeck(deck1, p1), CopyDeck(deck2, p2)) then
                table.insert(deck1, p1)
                table.insert(deck1, p2)
            else
                table.insert(deck2, p2)
                table.insert(deck2, p1)
            end
        else
            if (p1 > p2) then
                table.insert(deck1, p1)
                table.insert(deck1, p2)
            else
                table.insert(deck2, p2)
                table.insert(deck2, p1)
            end
        end
    end
    
    return #deck1 > 0
end

if PlayGame(player1, player2) then
    current = player1
else
    current = player2
end

local result = 0
for i, v in ipairs(current) do
    result = result + (#current - i + 1) * v
end
print("Part 2 ", result)
