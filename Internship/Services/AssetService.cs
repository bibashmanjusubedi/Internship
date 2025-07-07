using Internship.DAL.Repositories;

public class AssetService
{
    private readonly AssetRepository _assetRepository;

    public AssetService(AssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<int> GetAssetCountAsync()
    {
        return await _assetRepository.GetAssetCountAsync();
    }
}