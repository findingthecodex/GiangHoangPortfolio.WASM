using Microsoft.JSInterop;

namespace GiangHoangPortfolio.WASM.Services;

/// <summary>
/// Persists visit/scan/download counters in the browser's localStorage via JSInterop.
/// All counters are per-device. Keys are prefixed with "portfolio_".
/// </summary>
public class CounterService
{
    private readonly IJSRuntime _js;

    private const string KeyVisits      = "portfolio_visits";
    private const string KeyQrScans     = "portfolio_qr_scans";
    private const string KeyCvDownloads = "portfolio_cv_downloads";

    public CounterService(IJSRuntime jsRuntime)
    {
        _js = jsRuntime;
    }

    // ── visits ─────────────────────────────────────────────
    /// <summary>Increments the visit counter and returns the new value.</summary>
    public async ValueTask<int> IncrementVisitsAsync()
    {
        int current = await GetAsync(KeyVisits);
        int next    = current + 1;
        await SetAsync(KeyVisits, next);
        return next;
    }

    public async ValueTask<int> GetVisitsAsync() => await GetAsync(KeyVisits);

    // ── QR scans ───────────────────────────────────────────
    /// <summary>Call this when the /QR page loads (= one scan).</summary>
    public async ValueTask<int> IncrementQrScansAsync()
    {
        int current = await GetAsync(KeyQrScans);
        int next    = current + 1;
        await SetAsync(KeyQrScans, next);
        return next;
    }

    public async ValueTask<int> GetQrScansAsync() => await GetAsync(KeyQrScans);

    // ── CV downloads ───────────────────────────────────────
    public async ValueTask<int> IncrementCvDownloadsAsync()
    {
        int current = await GetAsync(KeyCvDownloads);
        int next    = current + 1;
        await SetAsync(KeyCvDownloads, next);
        return next;
    }

    public async ValueTask<int> GetCvDownloadsAsync() => await GetAsync(KeyCvDownloads);

    // ── helpers ────────────────────────────────────────────
    private async ValueTask<int> GetAsync(string key)
    {
        string? raw = await _js.InvokeAsync<string?>("localStorage.getItem", key);
        return int.TryParse(raw, out int val) ? val : 0;
    }

    private async ValueTask SetAsync(string key, int value)
    {
        await _js.InvokeVoidAsync("localStorage.setItem", key, value.ToString());
    }
}
